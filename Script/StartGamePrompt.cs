using UnityEngine; // استفاده از کتابخانه اصلی یونیتی
using UnityEngine.UI; // برای کار با UI مثل Text
using UnityEngine.SceneManagement; // برای بارگذاری صحنه‌ها
using System.Collections; // برای استفاده از IEnumerator و Coroutine

// این کلاس پیام شروع بازی را نمایش می‌دهد و با زدن Enter، بازی را شروع می‌کند
public class StartGamePrompt : MonoBehaviour
{
    public Text startMessage;            // شیء متنی که پیام شروع را نشان می‌دهد
    public AudioSource audioSource;      // منبع صدا برای پخش صوت شروع
    public AudioClip startClip;          // کلیپ صوتی که هنگام شروع بازی پخش می‌شود

    private bool gameStarted = false;    // مشخص می‌کند آیا بازی شروع شده یا نه

    void Start()
    {
        // اگر پیام شروع تنظیم شده باشد، آن را فعال کرده و شروع به چشمک زدن می‌کنیم
        if (startMessage != null)
        {
            startMessage.gameObject.SetActive(true); // فعال‌سازی متن
            StartCoroutine(BlinkText());             // اجرای چشمک زدن متن با کوروتین
        }

        // اگر منبع صدا تنظیم نشده باشد، هشدار می‌دهد
        if (audioSource == null)
            Debug.LogWarning("AudioSource تنظیم نشده!");
    }

    void Update()
    {
        // اگر بازی هنوز شروع نشده و کاربر دکمه Enter را بزند، بازی را شروع می‌کنیم
        if (!gameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            StartGame(); // تابع شروع بازی
        }
    }

    // تابعی برای شروع بازی
    void StartGame()
    {
        gameStarted = true; // علامت‌گذاری که بازی شروع شده

        // اگر پیام شروع وجود دارد، آن را مخفی می‌کنیم
        if (startMessage != null)
            startMessage.gameObject.SetActive(false);

        // اگر صدا و کلیپ تنظیم شده باشند، صدای شروع پخش می‌شود
        if (audioSource != null && startClip != null)
            audioSource.PlayOneShot(startClip);

        Debug.Log("بازی با موسیقی شروع شد");

        // اگر بخواهی صحنه جدیدی بارگذاری شود، خط زیر را از حالت کامنت خارج کن
        // SceneManager.LoadScene("GameScene"); // جای "GameScene" نام صحنه خودت را بگذار
    }

    // کوروتین برای چشمک زدن متن
    IEnumerator BlinkText()
    {
        while (!gameStarted) // تا وقتی بازی شروع نشده
        {
            startMessage.enabled = !startMessage.enabled; // خاموش و روشن کردن متن
            yield return new WaitForSeconds(0.5f);       // مکث نیم ثانیه
        }

        // وقتی بازی شروع شد، مطمئن شو متن غیرفعاله
        startMessage.enabled = false;
    }
}
