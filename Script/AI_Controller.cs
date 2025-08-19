using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// کنترلر هوش مصنوعی برای پدال (راکت) در یک بازی پینگ‌پنگ مانند
public class AI_Controller : MonoBehaviour
{
    public float baseSpeed = 10f;            // سرعت پایه حرکت پدال
    public Transform ball;                   // ترنسفورم توپ برای موقعیت فعلی آن
    public Rigidbody ballRb;                 // ریجیدبادی توپ برای گرفتن سرعت آن
    public float offset = 0.3f;              // میزان تحمل برای توقف حرکت (نزدیکی به محل هدف)
    public bool isRightSide = true;          // مشخص می‌کند این پدال در سمت راست است یا چپ

    private Rigidbody rb;                    // ریجیدبادی پدال برای اعمال سرعت

    void Start()
    {
        // دریافت ریجیدبادی پدال در شروع بازی
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // بررسی اینکه آیا توپ به سمت پدال در حال حرکت است یا نه
        bool isBallComing = (isRightSide && ballRb.velocity.x > 0) ||
                            (!isRightSide && ballRb.velocity.x < 0);

        if (isBallComing)
        {
            // اگر توپ به سمت پدال در حال حرکت باشد، شروع به پیش‌بینی و حرکت کن
            PredictAndMove();
        }
        else
        {
            // اگر توپ دور می‌شود، حرکت نکن (ثابت بایست)
            rb.velocity = Vector3.zero;
        }
    }

    // این تابع محل احتمالی برخورد توپ را پیش‌بینی می‌کند و پدال را به آن سمت حرکت می‌دهد
    private void PredictAndMove()
    {
        // فاصله بین توپ و پدال در محور x
        float distance = Mathf.Abs(ball.position.x - transform.position.x);

        // زمان تقریبی برخورد بر اساس سرعت توپ و فاصله
        float dynamicPredictionTime = Mathf.Clamp(distance / Mathf.Max(Mathf.Abs(ballRb.velocity.x), 0.1f), 0.1f, 1.5f);

        // محاسبه موقعیت احتمالی توپ بعد از dynamicPredictionTime ثانیه
        Vector3 predictedPosition = ball.position + ballRb.velocity * dynamicPredictionTime;

        // محور z هدف برای حرکت پدال
        float targetZ = predictedPosition.z;

        // تعیین ضریب سرعت متناسب با نزدیکی به توپ
        float speedFactor = Mathf.Clamp01(1f - distance / 20f);

        // افزایش سرعت دینامیکی تا حداکثر دو برابر
        float dynamicSpeed = Mathf.Lerp(baseSpeed, baseSpeed * 2f, speedFactor);

        // اگر پدال به اندازه‌ی offset به محل هدف نرسیده باشد، حرکت کن
        if (Mathf.Abs(transform.position.z - targetZ) > offset)
        {
            if (transform.position.z < targetZ)
                rb.velocity = Vector3.forward * dynamicSpeed; // حرکت به جلو
            else
                rb.velocity = Vector3.back * dynamicSpeed;    // حرکت به عقب
        }
        else
        {
            // اگر در نزدیکی محل هدف هستیم، توقف کن
            rb.velocity = Vector3.zero;
        }
    }
}
