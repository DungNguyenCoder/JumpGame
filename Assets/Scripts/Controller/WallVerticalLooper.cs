using System.Collections.Generic;
using UnityEngine;

public class WallVerticalLooper : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject _wall;
    [SerializeField] private GameObject _walls;
    private List<Transform> segments = new List<Transform>();
    [SerializeField] private bool _isPlaceOnRight = false;
    private float insetX = -0.5f;
    [SerializeField] private float extraBuffer = 0.2f;
    private float segmentHeight;
    void Awake()
    {
        ApplyWall();
        InitWall();
        if (cameraTransform == null) cameraTransform = Camera.main.transform;

        if (segments == null || segments.Count == 0) return;

        var sr = segments[0].GetComponent<SpriteRenderer>();
        segmentHeight = sr.bounds.size.y;

        float x = GetWallX();

        for (int i = 0; i < segments.Count; i++)
        {
            Vector3 p = segments[i].position;
            segments[i].position = new Vector3(x, p.y - i * segmentHeight, p.z);
        }
    }

    void InitWall()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject wall = Instantiate(_wall, _walls.transform);
            if (_isPlaceOnRight)
            {
                wall.transform.position = new Vector3(5, 4, 0);
                wall.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else
            {
                wall.transform.position = new Vector3(-5, 4, 0);
            }
            segments.Add(wall.transform);
        }
    }

    void LateUpdate()
    {
        var cam = Camera.main;
        if (cam == null || segments == null || segments.Count == 0) return;

        float camHeight = 2f * cam.orthographicSize;
        float camTop = cameraTransform.position.y + camHeight * 0.5f + extraBuffer;

        float x = GetWallX();

        for (int i = 0; i < segments.Count; i++)
        {
            var seg = segments[i];

            if (Mathf.Abs(seg.position.x - x) > 0.0001f)
                seg.position = new Vector3(x, seg.position.y, seg.position.z);

            float segBottom = seg.position.y - segmentHeight * 0.5f;

            if (segBottom < camTop)
                continue;

            float lowestY = segments[0].position.y;
            for (int j = 1; j < segments.Count; j++)
            {
                if (segments[j].position.y < lowestY)
                    lowestY = segments[j].position.y;
            }

            seg.position = new Vector3(x, lowestY - segmentHeight, seg.position.z);
        }
    }
    private float GetWallX()
    {
        var cam = Camera.main;
        float halfH = cam.orthographicSize;
        float halfW = halfH * cam.aspect;
        float camX = cameraTransform.position.x;

        float edge = _isPlaceOnRight ? camX + halfW : camX - halfW;
        return _isPlaceOnRight ? (edge - insetX) : (edge + insetX);
    }

    private void ApplyWall()
    {
        string mapName = PlayerPrefs.GetString(GameConfig.SELECTED_MAP_KEY, "");
        if (string.IsNullOrEmpty(mapName)) return;

        MapData data = Resources.Load<MapData>(GameConfig.MAP_DATA_PATH + mapName);
        if (data != null)
        {
            var sr = _wall.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = data.wall;
            }
        }
    }
}
