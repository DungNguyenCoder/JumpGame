using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GroundPool _groundPool;
    [SerializeField] private Transform _groundStart;
    [SerializeField] private int _targetGroundCount = 5;
    [SerializeField] private float _groundSpacingY = 2.5f;
    [SerializeField] private float _cameraYOffSet = -2f;
    private List<Ground> _activeGrounds = new List<Ground>();
    private int _totalGroundSpawned = 0;
    private void Start()
    {
        for (int i = 0; i < _targetGroundCount; i++)
        {
            SpawnGround();
        }
    }
    private void Update()
    {
        _activeGrounds.RemoveAll(g => g == null || !g.gameObject.activeInHierarchy);

        while (_activeGrounds.Count < _targetGroundCount)
        {
            SpawnGround();
        }
    }
    private void SpawnGround()
    {
        Ground newGround = _groundPool.GetGround();
        Vector3 spawnPosition = _groundStart.position + Vector3.down * _groundSpacingY * _totalGroundSpawned;
        newGround.transform.position = spawnPosition;
        _activeGrounds.Add(newGround);
        ++_totalGroundSpawned;
    }

    public void PlayerLanded(Ground landerGround)
    {
        float landedY = landerGround.transform.position.y;

        for (int i = _activeGrounds.Count - 1; i >= 0; i--)
        {
            if (_activeGrounds[i] == null)
                continue;
            if (_activeGrounds[i].transform.position.y > landedY)
            {
                _activeGrounds[i].gameObject.SetActive(false);
                _activeGrounds.RemoveAt(i);
            }
        }

        CameraController cam = Camera.main.GetComponent<CameraController>();
        if (cam != null)
        {
            cam.MoveToY(landedY + _cameraYOffSet);
        }

        while (_activeGrounds.Count < _targetGroundCount)
        {
            SpawnGround();
        }
    }
}
