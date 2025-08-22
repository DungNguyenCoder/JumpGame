using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    private void Update()
    {
        score.text = GameManager.Instance._score.ToString();
    }
}
