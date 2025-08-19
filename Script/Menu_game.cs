using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu_game : MonoBehaviour
{
    public void Play_Game_AI_Hard()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }


      public void Play_Game_Play_game_2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }





     public void Play_Game_AI_Normal()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }


    public void ExitGame(){
                  #if UNITY_EDITOR
        EditorApplication.isPlaying = false; // خروج در حالت تست یونیتی
        #else
        Application.Quit(); // خروج در نسخه نهایی Build
        #endif

        Debug.Log("بازی بسته شد!");
    }



}
