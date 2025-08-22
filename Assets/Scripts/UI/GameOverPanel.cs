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
        GameManager.Instance.RestartGame();
    }
    public void OnClickMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnEnable()
    {
        score.text = GameManager.Instance._score.ToString();
    }
}
