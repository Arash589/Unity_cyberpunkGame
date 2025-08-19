using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // برای استفاده از UI مثل Text

// این کلاس کنترل اصلی بازی را بر عهده دارد: امتیازگیری، شروع بازی و بازنشانی توپ
public class Game_Controller : MonoBehaviour
{
    public GameObject ball;               // شیء توپ بازی
    public Text scoreTextLeft;            // متن مربوط به امتیاز بازیکن چپ
    public Text scoreTextRight;           // متن مربوط به امتیاز بازیکن راست

    private int scoreLeft = 0;            // امتیاز بازیکن چپ
    private int scoreRight = 0;           // امتیاز بازیکن راست
    private Vector3 startingPosition;     // موقعیت اولیه‌ی توپ
    private Ball__controller ballController; // اسکریپت کنترل توپ (برای پرتاب و ریست)

    void Start()
    {
        // گرفتن رفرنس به اسکریپت کنترل توپ
        ballController = ball.GetComponent<Ball__controller>();

        // ذخیره کردن موقعیت اولیه توپ برای استفاده هنگام ریست
        startingPosition = ball.transform.position;
    }

    void Update()
    {
        // اگر کاربر دکمه Enter را بزند، توپ به حرکت درمی‌آید
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ballController.LaunchBall(); // پرتاب توپ
        }
    }

    // وقتی بازیکن سمت چپ گل می‌خورد (گل برای بازیکن راست)
    public void ScoreGoalLeft()
    {
        Debug.Log("ScoreGoalLeft"); // چاپ در کنسول برای تست
        scoreRight++; // افزایش امتیاز بازیکن راست
        UpdateUI();   // به‌روزرسانی متن امتیازها
        ballController.ResetBall(startingPosition); // برگرداندن توپ به وسط
    }

    // وقتی بازیکن سمت راست گل می‌خورد (گل برای بازیکن چپ)
    public void ScoreGoalRight()
    {
        Debug.Log("ScoreGoalRight");
        scoreLeft++;  // افزایش امتیاز بازیکن چپ
        UpdateUI();   // به‌روزرسانی متن امتیازها
        ballController.ResetBall(startingPosition);
    }

    // به‌روزرسانی متن‌های مربوط به امتیاز
    private void UpdateUI()
    {
        scoreTextLeft.text = scoreLeft.ToString();   // تبدیل عدد به متن برای نمایش
        scoreTextRight.text = scoreRight.ToString();
    }
}
