using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highScore;

    private void Start()
    {
        int best = PlayerPrefs.GetInt(GameConfig.HIGH_SCORE_KEY, 0);
        _highScore.text = best.ToString();
    }

    public void OnClickPlayButton()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
