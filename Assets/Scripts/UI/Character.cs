using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    [SerializeField] private Image _characterIcon;
    private CharacterData _data;
    private CharacterPanel _panel;

    public void Setup(CharacterData data, CharacterPanel panel)
    {
        _data = data;
        _panel = panel;
        _characterIcon.sprite = data.spriteIcon;
    }

    public void OnClick()
    {
        if (_panel != null)
        {
            _panel.SelectCharacter(_data);
        }
    }
}