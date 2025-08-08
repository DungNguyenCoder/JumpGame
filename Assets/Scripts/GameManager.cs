using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int _score = -1;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Time.timeScale = 1f;
    }

    public void AddScore()
    {
        _score++;
        Debug.Log("Score: " + _score);
    }

    public void GameOver()
    {
        Debug.LogError("Game Over");
        Time.timeScale = 0f;
    }
}