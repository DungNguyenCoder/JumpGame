using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private Character _characterPrefab;
    [SerializeField] private GameObject _board;
    [SerializeField] private Image _characterImageDemo;
    [SerializeField] private TextMeshProUGUI _characterName;
    private void Start()
    {
        LoadAvatar();
    }

    private void OnEnable()
    {
        string saved = PlayerPrefs.GetString(GameConfig.SELECTED_CHARACTER_KEY, "");
        if (!string.IsNullOrEmpty(saved))
        {
            CharacterData data = Resources.Load<CharacterData>(GameConfig.CHARACTER_DATA_PATH + saved);
            if (data != null)
            {
                _characterImageDemo.sprite = data.spriteDemo;
                _characterName.text = data.characterName;
            }
        }
    }

    public void LoadAvatar()
    {
        CharacterData[] characterDatas = Resources.LoadAll<CharacterData>(GameConfig.CHARACTER_DATA_PATH);
        foreach (CharacterData characterData in characterDatas)
        {
            Character character = Instantiate(_characterPrefab, _board.transform);
            character.Setup(characterData, this);
        }
    }

    public void SelectCharacter(CharacterData data)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        _characterImageDemo.sprite = data.spriteDemo;
        _characterName.text = data.characterName;
        PlayerPrefs.SetString(GameConfig.SELECTED_CHARACTER_KEY, data.name);
        PlayerPrefs.Save();
    }
}
