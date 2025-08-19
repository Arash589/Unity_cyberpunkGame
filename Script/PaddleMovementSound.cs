using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaddleMovementSound : MonoBehaviour
{
    // این متغیر برای نگهداری کامپوننت AudioSource راکت استفاده می‌شود
    private AudioSource audioSource;

    // این متغیر برای نگهداری فایل صوتی که می‌خواهیم پخش شود، استفاده می‌شود
    public AudioClip moveSound;

    // حداقل فاصله ای که راکت باید حرکت کند تا صدا پخش شود
    public float minMovementThreshold = 0.1f;

    // متغیر برای ذخیره موقعیت قبلی راکت
    private Vector3 lastPosition;

    void Awake()
    {
        // در متد Awake، AudioSource را دریافت می‌کنیم
        audioSource = GetComponent<AudioSource>();

        // اگر AudioSource پیدا نشد، یک هشدار نمایش می‌دهیم
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource component not found on this GameObject. Please add one.", this);
        }

        // موقعیت اولیه راکت را ذخیره می‌کنیم
        lastPosition = transform.position;
    }

    void Update()
    {
        // محاسبه فاصله ای که راکت از فریم قبلی حرکت کرده است
        float movementDistance = Vector3.Distance(transform.position, lastPosition);

        // اگر راکت به اندازه کافی حرکت کرده باشد و صدا در حال پخش نباشد
        if (movementDistance > minMovementThreshold && !audioSource.isPlaying)
        {
            // اطمینان حاصل می‌کنیم که AudioSource و فایل صوتی هر دو موجود هستند
            if (audioSource != null && moveSound != null)
            {
                // صدای حرکت را پخش می‌کنیم
                audioSource.PlayOneShot(moveSound);
            }
            else
            {
                // اگر فایل صوتی یا AudioSource تنظیم نشده باشد، هشدار می‌دهیم
                Debug.LogWarning("Move sound or AudioSource is not assigned.", this);
            }
        }

        // موقعیت فعلی را برای فریم بعدی ذخیره می‌کنیم
        lastPosition = transform.position;
    }
}