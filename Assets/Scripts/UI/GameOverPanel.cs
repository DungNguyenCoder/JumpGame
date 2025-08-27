using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : Panel
{
    [SerializeField] private TextMeshProUGUI score;
    public void OnClickRestartButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        GameManager.Instance.RestartGame();
    }
    public void OnClickMainMenuButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        AudioManager.Instance.PlayMusicFromStart();
        SceneManager.LoadScene("MainMenu");
    }

    private void OnEnable()
    {
        score.text = GameManager.Instance._score.ToString();
    }
}
