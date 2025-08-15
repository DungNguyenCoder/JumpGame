using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private TextMeshProUGUI _inGameScore;
    private int _score = -2;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Time.timeScale = 1f;
    }

    public void AddScore(int score)
    {
        _score += score;
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (_inGameScore != null)
        {
            _inGameScore.text = _score.ToString();
        }
    }

    public void GameOver()
    {
        Debug.LogError("Game Over");
        Time.timeScale = 0f;
    }
}