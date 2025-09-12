using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using System;

class GameManager : Singleton<GameManager>
{
    public int _score { get; private set; }
    public int _highScore { get; private set; }
    private bool _isPause = false;
    private bool _isPerfect;
    public int _groundLevel { get; private set; }
    public override void Awake()
    {
        // PlayerPrefs.DeleteAll();
        _score = 0;
        _groundLevel = 0;
        LoadHighScore();
        base.Awake();
        Time.timeScale = 1f;
    }
    public void AddScore(int score)
    {
        _score += score;
        UpdateGroundLevel();
        TryToUpdateHighScore();
    }
    private void TryToUpdateHighScore()
    {
        if (_score > _highScore)
        {
            _highScore = _score;
            SaveHighScore();
        }
    }
    private void LoadHighScore()
    {
        _highScore = PlayerPrefs.GetInt(GameConfig.HIGH_SCORE_KEY, 0);
    }
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(GameConfig.HIGH_SCORE_KEY, _highScore);
        PlayerPrefs.Save();
    }

    public void GameOver()
    {
        AudioManager.Instance.PauseMusic();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.gameOver);
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_GAME_OVER);
        _score = 0;
        _groundLevel = 0;
        Time.timeScale = 0f;
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        AudioManager.Instance.PauseMusic();
        _isPause = true;
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_PAUSE_GAME);
    }
    public void ContinueGame()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.ContinueMusic();
        _isPause = false;
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_PAUSE_GAME);
    }
    public void RestartGame()
    {
        PanelManager.Instance.ClosePanel(GameConfig.PANEL_GAME_OVER);
        AudioManager.Instance.PlayMusicFromStart();
        Time.timeScale = 1f;
        _score = 0;
        _groundLevel = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Setting()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_SETTING);
    }

    public void Choose()
    {
        PanelManager.Instance.OpenPanel(GameConfig.PANEL_CHOOSE);
    }
    public void Credit()
    {
        Application.OpenURL(GameConfig.GITHUB_LINK);
    }

    public void SetPerfect(bool value)
    {
        this._isPerfect = value;
    }

    public bool GetPerfect()
    {
        return this._isPerfect;
    }

    public bool GetPause()
    {
        return this._isPause;
    }

    private void UpdateGroundLevel()
    {
        if (_score < 10) _groundLevel = 0;
        else if (_score < 20) _groundLevel = 1;
        else if (_score < 30) _groundLevel = 2;
        else                 _groundLevel = 3;
    }
}