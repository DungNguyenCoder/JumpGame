using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private GameObject perfect;
    private float _time = 0f;

    private void Start()
    {
        if (!perfect)
        {
            perfect.SetActive(false);
        }
    }
    private void Update()
    {
        score.text = GameManager.Instance._score.ToString();
        if (GameManager.Instance.GetPerfect())
        {
            _time = 1f;
            GameManager.Instance.SetPerfect(false);
        }
        if (_time > 0f)
        {
            _time -= Time.deltaTime;
            if (!perfect.activeSelf) perfect.SetActive(true);
        }
        else
        {
            if (perfect.activeSelf) perfect.SetActive(false);
        }
    }
}
