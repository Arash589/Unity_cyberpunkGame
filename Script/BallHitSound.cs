using UnityEngine;

public class BallHitSound : MonoBehaviour
{
    public AudioClip hitSound; // فایل صوتی خود را اینجا در Inspector بکشید و رها کنید
    private AudioSource audioSource;

    void Awake()
    {
        // مطمئن می شویم که یک AudioSource روی این GameObject وجود دارد.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // تنظیمات اولیه AudioSource
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 1.0f; // 1.0f برای صدای 3D، 0.0f برای صدای 2D
        audioSource.volume = 0.7f;
    }

    // این تابع زمانی فراخوانی می شود که یک تریگر (Trigger) با یک تریگر دیگر یا یک Collider غیر-تریگر
    // (که حداقل یکی از آنها Rigidbody داشته باشد) وارد شود.
    void OnTriggerEnter(Collider other) // از Collider other برای تریگرها استفاده می شود.
    {
        // یک متغیر برای نگه داشتن نام شیءی که برخورد کرده ایم.
        string hitObjectName = other.gameObject.name;

        // بررسی می کنیم که آیا فایل صوتی اختصاص داده شده است یا خیر.
        if (hitSound != null)
        {
            // از PlayOneShot استفاده می کنیم تا صداها روی هم نیفتند و هر بار به طور کامل پخش شوند.
            audioSource.PlayOneShot(hitSound);
            Debug.Log("Ball triggered with: " + hitObjectName + ". Playing sound.");
        }
        else
        {
            Debug.LogWarning("Hit Sound AudioClip is not assigned on " + gameObject.name + "'s BallHitSound script.");
        }
    }

    // نکته مهم: اگر Is Trigger روی Collider توپ شما فعال باشد،
    // تابع OnCollisionEnter (که برای برخورد فیزیکی است) هرگز فراخوانی نمی شود.
    // بنابراین، مطمئن شوید که تابع OnCollisionEnter در اسکریپت شما وجود ندارد.
}