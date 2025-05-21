using System;
using UnityEngine;

public class GameOverDetector
{
    private static GameOverDetector Instance;

    public event Action GameEnded;

    private GameOverDetector() { }

    public static GameOverDetector GetGameOverDetection()
    {
        return Instance ??= new GameOverDetector();
    }

    public void EndGame()
    {
        GameEnded?.Invoke();
    }
}
