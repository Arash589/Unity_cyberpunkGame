using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Transform topViewPosition;        // موقعیت نمای بالا
    public Transform perspectivePosition;    // موقعیت نمای سه‌بعدی
    public float transitionSpeed = 5f;

    private Transform targetPosition;

    void Start()
    {
        // شروع در حالت نمای بالا (یا هرکدوم خواستی)
        targetPosition = topViewPosition;
    }

    void Update()
    {
        // تغییر هدف دوربین با کلید Q یا E
        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetPosition = topViewPosition;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            targetPosition = perspectivePosition;
        }

        // حرکت نرم دوربین
        transform.position = Vector3.Lerp(transform.position, targetPosition.position, Time.deltaTime * transitionSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetPosition.rotation, Time.deltaTime * transitionSpeed);
    }
}