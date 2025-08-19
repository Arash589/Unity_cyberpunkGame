using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// این اسکریپت کنترل حرکت راکت (راکت پینگ‌پنگ) را به دو صورت بازیکن یا هوش مصنوعی (AI) بر عهده دارد
public class Racet_controller : MonoBehaviour
{
    public float speed;                      // سرعت حرکت پایه‌ی راکت
    public KeyCode up;                       // کلید حرکت به بالا
    public KeyCode down;                     // کلید حرکت به پایین
    public bool isPlayer = true;             // مشخص می‌کند که این راکت توسط بازیکن کنترل می‌شود یا AI
    public float offset = 0.1f;              // حساسیت دنبال کردن توپ برای AI
    public float normalSpeed = 10f;          // سرعت عادی راکت
    public float boostedSpeed = 18f;         // سرعت بیشتر وقتی دکمه افزایش سرعت فشار داده شود
    public KeyCode boostKey = KeyCode.LeftShift; // کلید برای فعال‌سازی سرعت بیشتر (Boost)

    private Rigidbody rb;                    // Rigidbody برای اعمال فیزیک حرکت
    private Transform ball;                  // موقعیت توپ برای دنبال کردن توسط AI

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // گرفتن Rigidbody شیء فعلی
        ball = GameObject.FindGameObjectWithTag("Ball").transform; // پیدا کردن توپ از طریق تگ
    }

    void FixedUpdate()
    {
        float input = 0f;

        // کنترل حرکت بازیکن با کلیدهای W و S روی محور Z
        if (Input.GetKey(KeyCode.W))
            input = 1f;
        else if (Input.GetKey(KeyCode.S))
            input = -1f;

        // اگر کلید Boost (مثلاً Shift) نگه داشته شود، از سرعت بیشتر استفاده می‌شود
        float speed = Input.GetKey(boostKey) ? boostedSpeed : normalSpeed;

        // ساخت بردار سرعت و اعمال آن به راکت
        Vector3 velocity = new Vector3(0f, 0f, input * speed);
        rb.velocity = velocity;
    }

    void Update()
    {
        // اگر این راکت بازیکن باشد، با کلیدها کنترل می‌شود
        if (this.isPlayer)
        {
            MoveByPlayer();
        }
        // اگر AI باشد، توپ را دنبال می‌کند
        else
        {
            MoveByComputer();
        }
    }

    // حرکت راکت توسط بازیکن با کلیدهای تعیین‌شده
    private void MoveByPlayer()
    {
        bool pressedUp = Input.GetKey(this.up);      // بررسی فشار کلید بالا
        bool pressedDown = Input.GetKey(this.down);  // بررسی فشار کلید پایین

        if (pressedUp && pressedDown)
        {
            rb.velocity = Vector3.zero; // اگر هر دو کلید با هم فشرده شده باشند، راکت نمی‌جنبد
        }
        else if (pressedUp)
        {
            rb.velocity = Vector3.forward * speed; // حرکت به جلو (بالا)
        }
        else if (pressedDown)
        {
            rb.velocity = Vector3.back * speed;    // حرکت به عقب (پایین)
        }
        else
        {
            rb.velocity = Vector3.zero; // اگر هیچ کلیدی فشرده نشده باشد، راکت نمی‌جنبد
        }
    }

    // حرکت راکت توسط هوش مصنوعی (AI) برای دنبال کردن موقعیت توپ
    private void MoveByComputer()
    {
        // اگر توپ جلوتر از راکت باشد با در نظر گرفتن offset، حرکت به جلو
        if (ball.position.z > transform.position.z + offset)
        {
            rb.velocity = Vector3.forward * speed;
        }
        // اگر توپ عقب‌تر از راکت باشد، حرکت به عقب
        else if (ball.position.z < transform.position.z - offset)
        {
            rb.velocity = Vector3.back * speed;
        }
        // اگر توپ تقریباً در یک سطح با راکت باشد، راکت متوقف می‌شود
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
