using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    TextMeshProUGUI timeText;
    float time = 0;
    bool keepTime = true;

    private void Awake()
    {
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

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);

        if (minutes > 59)
        {
            int hours = Mathf.FloorToInt(time / 3600);
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
