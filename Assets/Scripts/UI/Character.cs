using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Image _characterIcon;
    public void SetCharacterIcon(Sprite characterSprite)
    {
        _characterIcon.sprite = characterSprite;
    }
}