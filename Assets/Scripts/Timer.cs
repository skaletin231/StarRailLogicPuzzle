using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timeText;
    static float time = 0;
    bool keepTime = true;
    static int hours;
    static int minutes;
    static int seconds;

    public static float GetHours()
    {
        //Debug.Log(TimeSpan.FromSeconds(Mathf.Round(time)));
        return hours;
    }
    public static float GetMinutes()
    {
        return minutes;
    }
    public static float GetSeconds()
    {
        return seconds;
    }

    private void Awake()
    {
        time = 0;
        timeText = GetComponent<TextMeshProUGUI>();
        GameOverDetector.GetGameOverDetection().GameEnded += GameEnded;
    }

    private void Update()
    {
        if (!keepTime)
        {
            return;
        }

        time += Time.deltaTime;

        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);

        if (minutes > 59)
        {
            hours = Mathf.FloorToInt(time / 3600);
            minutes = minutes % 60;
            timeText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
        else
        {
            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void GameEnded()
    {
        keepTime = false;
    }
}
