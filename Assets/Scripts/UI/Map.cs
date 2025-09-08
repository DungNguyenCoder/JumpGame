using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Map : MonoBehaviour
{
    [SerializeField] private Image mapIcon;
    [SerializeField] private GameObject check;
    private MapData _data;
    private MapPanel _panel;
    public string DataName => _data != null ? _data.name : "";

    public void Setup(MapData data, MapPanel panel)
    {
        _data = data;
        _panel = panel;
        mapIcon.sprite = data.mapIcon;
        check.SetActive(false);
    }

    public void OnClick()
    {
        if (_panel != null)
        {
            _panel.SelectMap(_data, this);
        }
    }
    public void SetCheck(bool value)
    {
        check.SetActive(value);
    }
}