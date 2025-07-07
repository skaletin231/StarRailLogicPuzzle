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
        scoreText.text = string.Format("Final Score: {0:0}/{1:00}",ScoreTracker.GetScoreTracker().Score(), ScoreTracker.GetScoreTracker().Total()) ;
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
