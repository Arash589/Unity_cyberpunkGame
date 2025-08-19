using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// این اسکریپت برای کنترل راکت بازیکن دوم (PLAYER2) است — می‌تواند توسط بازیکن یا هوش مصنوعی کنترل شود
public class PLAYER2 : MonoBehaviour
{
    public float speed;                         // سرعت حرکت راکت
    public KeyCode up;                          // کلید حرکت به بالا
    public KeyCode down;                        // کلید حرکت به پایین
    public bool isPlayer = true;                // مشخص می‌کند که راکت توسط بازیکن کنترل می‌شود یا هوش مصنوعی
    public float offset = 0.1f;                 // حساسیت حرکت AI به سمت توپ
    public float normalSpeed = 10f;             // سرعت عادی راکت
    public float boostedSpeed = 18f;            // سرعت زمانی که دکمه Boost فشرده شود
    public KeyCode boostKey = KeyCode.LeftShift;// کلید افزایش سرعت (پیش‌فرض: Shift چپ)

    private Rigidbody rb;                       // Rigidbody راکت برای اعمال حرکت
    private Transform ball;                     // مرجع به شی توپ (برای AI جهت دنبال کردن)

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // گرفتن Rigidbody این آبجکت
        ball = GameObject.FindGameObjectWithTag("Ball").transform; // پیدا کردن توپ از طریق تگ "Ball"
    }

    void FixedUpdate()
    {
        float input = 0f;

        // اگر کاربر کلید ↑ یا ↓ را بزند، ورودی تنظیم می‌شود
        if (Input.GetKey(KeyCode.UpArrow))
            input = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            input = -1f;

        // اگر کلید Boost نگه داشته شود، از سرعت بیشتر استفاده می‌شود
        float speed = Input.GetKey(boostKey) ? boostedSpeed : normalSpeed;

        // تنظیم بردار حرکت راکت در محور Z (بالا-پایین)
        Vector3 velocity = new Vector3(0f, 0f, input * speed);
        rb.velocity = velocity;
    }

    void Update()
    {
        // بررسی می‌شود که آیا راکت توسط بازیکن کنترل می‌شود یا نه
        if (this.isPlayer)
        {
            MoveByPlayer(); // کنترل دستی
        }
        else
        {
            MoveByComputer(); // کنترل خودکار (AI)
        }
    }

    // حرکت راکت با کنترل دستی توسط کاربر
    private void MoveByPlayer()
    {
        bool pressedUp = Input.GetKey(this.up);        // آیا کلید بالا فشار داده شده؟
        bool pressedDown = Input.GetKey(this.down);    // آیا کلید پایین فشار داده شده؟

        if (pressedUp && pressedDown)
        {
            rb.velocity = Vector3.zero; // اگر هر دو کلید با هم زده شوند، راکت نمی‌جنبد
        }
        else if (pressedUp)
        {
            rb.velocity = Vector3.forward * speed; // حرکت رو به بالا (محور Z)
        }
        else if (pressedDown)
        {
            rb.velocity = Vector3.back * speed;    // حرکت رو به پایین (محور Z منفی)
        }
        else
        {
            rb.velocity = Vector3.zero; // اگر هیچ کلیدی فشرده نشود، راکت متوقف می‌شود
        }
    }

    // حرکت راکت به صورت خودکار توسط AI (هوش مصنوعی)
    private void MoveByComputer()
    {
        if (ball.position.z > transform.position.z + offset)
        {
            rb.velocity = Vector3.forward * speed; // اگر توپ جلوتر باشد، راکت جلو می‌رود
        }
        else if (ball.position.z < transform.position.z - offset)
        {
            rb.velocity = Vector3.back * speed; // اگر توپ عقب‌تر باشد، راکت عقب می‌رود
        }
        else
        {
            rb.velocity = Vector3.zero; // اگر توپ نزدیک باشد، راکت ثابت می‌ماند
        }
    }
}
