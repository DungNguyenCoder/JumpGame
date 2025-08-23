using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "CharacterData", order = 0)]
public class CharacterData : ScriptableObject
{
    public Sprite spriteIcon;
    public Sprite spriteDemo;
    public string characterName;
}