using UnityEngine; // استفاده از کتابخانه‌ی یونیتی برای کار با اجزای اصلی مانند ترنسفورم، فیزیک و غیره
using UnityEngine.Events; // استفاده از سیستم رویدادهای یونیتی برای تعریف UnityEvent

// این کلاس وظیفه دارد زمانی که توپ وارد دروازه شد، رویدادی را اجرا کند
public class Goal_Controller : MonoBehaviour
{
    public UnityEvent onGoalScored; // رویدادی که وقتی گل زده شد فعال می‌شود (می‌توان از طریق Inspector تنظیم کرد)

    // این تابع زمانی اجرا می‌شود که شی‌ای وارد محدوده‌ی Trigger این آبجکت شود
    private void OnTriggerEnter(Collider other)
    {
        // بررسی می‌کنیم که آیا شیء وارد شده، تگ "Ball" دارد یا نه (یعنی آیا توپ وارد دروازه شده؟)
        if (other.CompareTag("Ball"))
        {
            // اگر رویداد تنظیم شده باشد (خالی نباشد)
            if (onGoalScored != null)
            {
                // اجرای رویداد (مثلاً افزایش امتیاز، پخش صدا، یا شروع انیمیشن)
                onGoalScored.Invoke();
            }
        }
    }
}
