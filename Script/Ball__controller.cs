using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ball__controller : MonoBehaviour
{
    public float initialSpeed = 99.2f;             // سرعت نهایی در طول بازی
    public float launchSpeed = 20f;                //  سرعت کند هنگام شروع یا گل
    public float minDirection = 0.3f;              //حداقل انحراف سرعت   
    public float speedIncreaseFactor = 10.1f;       //ضریب افزایش سرعت بعد از برخورد
    public float maxSpeed = 100f;                  //سرعت مجاز آن 100 هست 
    public GameObject sparksVFX;                   // افکت جرقه هنگام بازی 
    private bool stopped = false;                // مشخص می کند توپ متوقف شده یا نه 
    private float currentSpeed;                    //سرعت فعلی توپ
    private Vector3 direction;                     //جهت حرکت توپ 
    private Rigidbody rb;                          //بدنه توپ هست 

    void Start()
    {
        rb = GetComponent<Rigidbody>();         //توپ را دریافت میکنم 
        direction = Vector3.zero;               //جهت حرکت را صفر می کنم
        currentSpeed = 0f;                      // سرعت را صفر می کنم
        stopped = true;                         // توپ را متوقف می کنیم
    }

    void FixedUpdate()
    {
        // اگر توپ در حال حرکت باشد 
        if (!stopped)
        {
            direction.y = 0;//اطمینان حاصل می کنیم که حرکت فقط در محور افقی x,y باشد
            rb.MovePosition(rb.position + direction * currentSpeed * Time.fixedDeltaTime); // توپ را براساس جهت و سرعت به مکان جدید منتقل می کنیم
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bool hitOccurred = false; // زمانی اجرا می شود توپ به دیوار یا راکت بخورد این متغییر بررسی آیا برخوردی صورت کرفته یا نه 

        if (other.CompareTag("Wall"))
        {
            direction.z = -direction.z; // اکر توپ به دیوار برخورد میکنه جهت آن در محور Z معکوس می شود 
            hitOccurred = true;
        }

        if (other.CompareTag("Racket"))//اکر توپ به راکت بازیکن برخورد کند 
        {
            Vector3 newDirection = (transform.position - other.transform.position);// جهت توپ ار موقغیت توپ نسبت به راکت محاسبه می شود
            newDirection.y = 0;                                                    //جهت حرکت حداقل انحراف را دارد
            newDirection = newDirection.normalized;                                //سرعت توپ را ضرایب افزایش یافته ولی حداکثر سرعت زیاد نمی شود 
            newDirection.x = Mathf.Sign(newDirection.x) * Mathf.Max(Mathf.Abs(newDirection.x), minDirection);
            newDirection.z = Mathf.Sign(newDirection.z) * Mathf.Max(Mathf.Abs(newDirection.z), minDirection);
            direction = newDirection;
            hitOccurred = true;
            currentSpeed = Mathf.Min(currentSpeed * speedIncreaseFactor, maxSpeed); // افزایش سرعت بعد از برخورد با راکت
        }

        if(hitOccurred){ //اکر برخورد رخ داد و افکت جرقه در محل برخورد ظاهر شده و پس از 4 ثانیه حذف شود 
            GameObject sparks=Instantiate(this.sparksVFX,transform.position,transform.rotation);
            Destroy(sparks,4f);
        }
    }
    public void LaunchBall()
    {
        stopped = false; // Allow movement when launching
        currentSpeed = launchSpeed; //  سرعت پایین هنگام زدن Enter
        ChooseDirection();
    }

    public void ResetBall(Vector3 startPos)//توپ رابه موقعیت مشخص شده می برد . دوباره راه انداز ی میکند 
    {
        transform.position = startPos;
        stopped = false; // Allow movement when resetting
        currentSpeed = launchSpeed; //  سرعت پایین بعد از گل
        ChooseDirection();
    }

    private void ChooseDirection()// یک جهت تصادفی برای شروع حرکت توپ انتخاب میکند تا همیشه سمت مشخص حرکت نکند 
    {
        float signX = Mathf.Sign(Random.Range(-1f, 1f));
        float signZ = Mathf.Sign(Random.Range(-1f, 1f));

        if (signX == 0) signX = 1;// مثبت 1 تبدیل میشه  اکر صفر بیاد  بدون جهت 
        if (signZ == 0) signZ = 1;

        direction = new Vector3(signX, 0, signZ).normalized;
    }
}