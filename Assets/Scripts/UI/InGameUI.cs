using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class InGameUI : Singleton<InGameUI>
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private GameObject perfect;
    [SerializeField] private TextMeshProUGUI readyText;
    private void Start()
    {
        perfect.SetActive(false);
        readyText.gameObject.SetActive(true);
        Time.timeScale = 0f;
        StartCoroutine(ReadyUI());
    }
    public void StartPerfectTime()
    {
        StartCoroutine(PerfectTime());
    }
    public void UpdateScore()
    {
        score.text = GameManager.Instance._score.ToString();
    }
    IEnumerator PerfectTime()
    {
        perfect.SetActive(true);
        yield return new WaitForSeconds(1f);
        perfect.SetActive(false);
    }
    public void OnClickPause()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        GameManager.Instance.PauseGame();
    }

    IEnumerator ReadyUI()
    {
        for (int i = 3; i > 0; i--)
        {
            readyText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        readyText.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
