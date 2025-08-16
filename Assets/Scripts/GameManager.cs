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
    private bool _isPause = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Key down");
            TogglePause();
        }
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
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.gameOver);
        Debug.LogError("Game Over");
        Time.timeScale = 0f;
    }
    private void TogglePause()
    {
        if (_isPause)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        AudioManager.Instance.StopMusic();
        _isPause = true;
        Debug.Log("Game pause");
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.ResumeMusic();
        _isPause = false;
        Debug.Log("Game resume");
    }
}