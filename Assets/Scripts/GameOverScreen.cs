using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] Button resetButton;
    [SerializeField] Button menuButton;
    [SerializeField] String menuScene;
    [SerializeField] String gameScene;

    private void Start()
    {
        GameOverDetector.GetGameOverDetection().GameEnded += GameEnded;
        gameObject.SetActive(false);
    }

    private void GameEnded()
    {
        gameObject.SetActive(true);
        scoreText.text = string.Format("Final Score: {0:0}/{1:00}",ScoreTracker.GetScoreTracker().Score(), ScoreTracker.GetScoreTracker().Total());

        if (Timer.GetHours() > 0)
        {
            timeText.text = string.Format("Final Time: {0:0}:{1:00}:{2:00}", Timer.GetHours(), Timer.GetMinutes(), Timer.GetSeconds());
        }
        else
        {
            timeText.text = string.Format("Final Time: {0:0}:{1:00}", Timer.GetMinutes(), Timer.GetSeconds());
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void Menu()
    {
        SceneManager.LoadScene(menuScene);
    }
}
