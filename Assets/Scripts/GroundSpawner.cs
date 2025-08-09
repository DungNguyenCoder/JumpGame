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
    private float _randomXPosition;
    private List<Ground> _activeGrounds = new List<Ground>();

    CameraController cam;
    private int _totalGroundSpawned = 0;
    private void Start()
    {
        for (int i = 0; i < _targetGroundCount; i++)
        {
            SpawnGround();
        }
        cam = Camera.main.GetComponent<CameraController>();
    }

    public void SpawnGround()
    {
        Ground newGround = _groundPool.GetGround();
        Vector3 spawnPosition = _groundStart.position + Vector3.down * _groundSpacingY * _totalGroundSpawned + Vector3.right * Random.Range(-2, 2);
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

        if (cam != null)
        {
            cam.MoveToY(landedY + _cameraYOffSet);
        }

        while (_activeGrounds.Count < _targetGroundCount)
        {
            SpawnGround();
        }
    }

    public List<Ground> ActiveGrounds
    {
        get { return _activeGrounds; }
        set { _activeGrounds = value; }
    }

    public int TargetGroundCount
    {
        get { return _targetGroundCount; }
        set { _targetGroundCount = value;}
    }
}
