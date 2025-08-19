using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event System.Action<int> OnScoreChanged;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameOverPanel;
    private int _score = -2;
    private bool _isPause = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
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
        OnScoreChanged?.Invoke(_score);
    }

    public void GameOver()
    {
        AudioManager.Instance.PauseMusic();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.gameOver);
        _gameOverPanel.SetActive(true);
        Debug.Log("Game Over");
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
        AudioManager.Instance.PauseMusic();
        _isPause = true;
        _pausePanel.SetActive(true);
        Debug.Log("Game pause");
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.ResumeMusic();
        _isPause = false;
        _pausePanel.SetActive(false);
        Debug.Log("Game resume");
    }
    public void RestartGame()
    {
        _gameOverPanel.SetActive(false);
        _pausePanel.SetActive(false);
        AudioManager.Instance.PlayMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}