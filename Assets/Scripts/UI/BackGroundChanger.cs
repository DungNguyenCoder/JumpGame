using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
    [SerializeField] private Image _backGround;
    private void Start()
    {
        ApplyBackground();
    }

    private void ApplyBackground()
    {
        string mapName = PlayerPrefs.GetString(GameConfig.SELECTED_MAP_KEY, "");
        if (string.IsNullOrEmpty(mapName)) return;

        MapData data = Resources.Load<MapData>(GameConfig.MAP_DATA_PATH + mapName);
        if (data != null && data.backGround != null)
        {
            if (_backGround != null)
            {
                _backGround.sprite = data.backGround;
                // Debug.Log("Loaded background");
            }
        }
    }
}