using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InGameUI : Singleton<InGameUI>
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private GameObject perfect;
    private void Start()
    {
        perfect.SetActive(false);
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
}
