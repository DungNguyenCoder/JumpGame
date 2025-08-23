using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private Character _characterPrefab;
    [SerializeField] private GameObject _board;
    private void Start()
    {
        LoadAvatar();
    }

    public void LoadAvatar()
    {
        CharacterData[] characterDatas = Resources.LoadAll<CharacterData>("Data/");
        foreach (CharacterData characterData in characterDatas)
        {
            Character character = Instantiate(_characterPrefab, _board.transform);
            character.SetCharacterIcon(characterData.spriteIcon);
        }
    } 
}
