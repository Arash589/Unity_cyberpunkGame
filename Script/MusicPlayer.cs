using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// این اسکریپت مدیریت پخش موسیقی در بازی را بر عهده دارد (پخش، توقف، رفتن به آهنگ بعدی یا قبلی)
public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;               // منبع صدا که موسیقی را پخش می‌کند
    public AudioClip[] musicTracks = new AudioClip[10]; // آرایه‌ای از آهنگ‌ها (می‌تونی تا ۱۰ آهنگ اضافه کنی)

    private int currentTrackIndex = 0;            // شماره آهنگ فعلی

    void Start()
    {
        PlayTrack(currentTrackIndex); // در شروع، اولین آهنگ را پخش کن
    }

    void Update()
    {
        // اگر دکمه P زده شود، پخش/توقف انجام شود
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayPause();
        }

        // اگر دکمه N زده شود، آهنگ بعدی پخش شود
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextTrack();
        }

        // اگر دکمه B زده شود، آهنگ قبلی پخش شود
        if (Input.GetKeyDown(KeyCode.B))
        {
            PreviousTrack();
        }
    }

    // پخش یا توقف آهنگ (toggle بین play و pause)
    public void PlayPause()
    {
        if (audioSource.isPlaying)
            audioSource.Pause(); // اگر در حال پخش است، توقف کند
        else
            audioSource.Play();  // در غیر اینصورت، پخش کند
    }

    // رفتن به آهنگ بعدی
    public void NextTrack()
    {
        currentTrackIndex++; // شماره آهنگ را افزایش می‌دهیم

        // اگر به انتهای لیست رسید، برگرد به اولین آهنگ
        if (currentTrackIndex >= musicTracks.Length)
            currentTrackIndex = 0;

        PlayTrack(currentTrackIndex); // آهنگ را پخش کن
    }

    // رفتن به آهنگ قبلی
    public void PreviousTrack()
    {
        currentTrackIndex--; // شماره آهنگ را کاهش می‌دهیم

        // اگر به اول رسیدیم، برو به آخرین آهنگ
        if (currentTrackIndex < 0)
            currentTrackIndex = musicTracks.Length - 1;

        PlayTrack(currentTrackIndex); // آهنگ را پخش کن
    }

    // پخش آهنگ مشخص‌شده بر اساس شماره (index)
    void PlayTrack(int index)
    {
        audioSource.clip = musicTracks[index]; // قرار دادن کلیپ انتخاب‌شده در AudioSource
        audioSource.Play();                    // پخش آهنگ
    }
}
