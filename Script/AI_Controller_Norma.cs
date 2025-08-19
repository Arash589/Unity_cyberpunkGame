using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// این اسکریپت برای کنترل هوش مصنوعی پَد (راکت) در بازی پینگ‌پنگ یا مشابه آن طراحی شده
public class AI_Controller_Norma : MonoBehaviour
{
    public float baseSpeed = 15f;         // سرعت پایه‌ی حرکت راکت
    public Transform ball;                // آبجکت توپ در صحنه
    public Rigidbody ballRb;              // Rigidbody توپ برای دسترسی به سرعت آن
    public float offset = 0.3f;           // محدوده‌ی بی‌حرکتی (اگر توپ خیلی نزدیک مرکز راکت بود، حرکتی نکن)
    public bool isRightSide = true;       // مشخص می‌کند که راکت در سمت راست است یا نه (برای فهم جهت توپ)
    private Rigidbody rb;                 // Rigidbody راکت

    void Start()
    {
        rb = GetComponent<Rigidbody>();   // گرفتن Rigidbody راکت در شروع
    }

    void FixedUpdate()
    {
        // بررسی اینکه آیا توپ دارد به سمت راکت می‌آید یا نه
        bool isBallComing = (isRightSide && ballRb.velocity.x > 0) ||  // اگر در سمت راست هست و توپ به سمت راست حرکت می‌کند
                            (!isRightSide && ballRb.velocity.x < 0);   // یا اگر در سمت چپ هست و توپ به سمت چپ می‌رود

        if (isBallComing)
        {
            PredictAndMove();  // اگر توپ به سمت راکت می‌آید، مسیرش را پیش‌بینی کرده و حرکت کن
        }
        else
        {
            rb.velocity = Vector3.zero;  // اگر توپ نمی‌آید، راکت را متوقف کن
        }
    }

    private void PredictAndMove()
    {
        // محاسبه‌ی فاصله‌ی افقی بین راکت و توپ
        float distance = Mathf.Abs(ball.position.x - transform.position.x);

        // محاسبه‌ی زمان تقریبی رسیدن توپ به سمت راکت با سرعت فعلی → و محدود کردنش به حد مشخصی
        float dynamicPredictionTime = Mathf.Clamp(distance / Mathf.Max(Mathf.Abs(ballRb.velocity.x), 0.1f), 0.1f, 0.1f);

        // موقعیت پیش‌بینی‌شده توپ در آینده
        Vector3 predictedPosition = ball.position + ballRb.velocity * dynamicPredictionTime;

        // گرفتن مختصات Z هدف (یعنی در راستای عمودی زمین بازی)
        float targetZ = predictedPosition.z;

        // محاسبه‌ی ضریب سرعت بسته به فاصله → هرچه نزدیک‌تر، سرعت بیشتر
        float speedFactor = Mathf.Clamp01(1f - distance / 6f);

        // محاسبه‌ی سرعت پویا بر اساس فاصله، ولی در اینجا سرعت تغییر نمی‌کند چون multiplier = 1f
        float dynamicSpeed = Mathf.Lerp(baseSpeed, baseSpeed * 1f, speedFactor);

        // اگر اختلاف موقعیت Z فعلی و هدف، بیشتر از offset بود، حرکت کن
        if (Mathf.Abs(transform.position.z - targetZ) > offset)
        {
            if (transform.position.z < targetZ)
                rb.velocity = Vector3.forward * dynamicSpeed;  // حرکت به بالا (در راستای +Z)
            else
                rb.velocity = Vector3.back * dynamicSpeed;     // حرکت به پایین (در راستای -Z)
        }
        else
        {
            rb.velocity = Vector3.zero;  // اگر خیلی نزدیک به هدف بود، نیازی به حرکت نیست
        }
    }
}
