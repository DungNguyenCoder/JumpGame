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
        AudioManager.Instance.PlayMusicFromStart();
        SceneManager.LoadScene("GamePlay");
    }

    public void OnClickSettingButton()
    {
        GameManager.Instance.Setting();
    }

    public void OnClickChooseButton()
    {
        GameManager.Instance.Choose();
    }

    public void OnClickNoAdsButton()
    {
        //todo
    }
}
