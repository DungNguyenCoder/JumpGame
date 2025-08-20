using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private TextMeshProUGUI _inGameScore;
    [SerializeField] private TextMeshProUGUI _gameOverScore;
    private int _score = -2;
    private bool _isPause = false;
    public override void Awake()
    {
        base.Awake();
        Time.timeScale = 1f;
    }

    public void AddScore(int score)
    {
        _score += score;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        _gameOverScore.text = _score.ToString();
        _inGameScore.text = _score.ToString();
    }

    public void GameOver()
    {
        AudioManager.Instance.PauseMusic();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.gameOver);
        _gameOverPanel.SetActive(true);
        Debug.Log("Game Over");
        Time.timeScale = 0f;
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