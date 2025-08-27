using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MapPanel : MonoBehaviour
{
    [SerializeField] private Map _mapPrefab;
    [SerializeField] private GameObject _board;

    private List<Map> _maps = new List<Map>();
    private void Start()
    {
        LoadAvatar();
    }

    public void LoadAvatar()
    {
        MapData[] mapDatas = Resources.LoadAll<MapData>(GameConfig.MAP_DATA_PATH);
        foreach (MapData data in mapDatas)
        {
            Map map = Instantiate(_mapPrefab, _board.transform);
            map.Setup(data, this);
            _maps.Add(map);
        }
    }

    public void SelectMap(MapData data, Map selectedMap)
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        foreach (Map map in _maps)
        {
            map.SetCheck(false);
        }
        selectedMap.SetCheck(true);
        PlayerPrefs.SetString(GameConfig.SELECTED_MAP_KEY, data.name);
        PlayerPrefs.Save();
    }
}
