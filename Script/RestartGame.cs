using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    void Update()
    {
        // بررسی اینکه آیا کلید R زده شده
        if (Input.GetKeyDown(KeyCode.R))
        {
            // گرفتن صحنه فعلی
            Scene currentScene = SceneManager.GetActiveScene();

            // بارگذاری مجدد صحنه فعلی (ری‌استارت)
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
