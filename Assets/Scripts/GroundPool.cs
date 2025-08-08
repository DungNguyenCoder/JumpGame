using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GroundPool : MonoBehaviour
{
    [SerializeField] private Ground _ground;

    private int amount = 10;
    private Ground[] _groundPool = new Ground[1000];

    private void Awake()
    {
        InitPool();
    }

    private void InitPool()
    {
        for (int i = 0; i <= 10; i++)
        {
            Ground ground = Instantiate(_ground);
            _groundPool[i] = ground;
            ground.gameObject.SetActive(false);
            Debug.Log("Init Ground");
        }
    }

    public Ground GetGround()
    {
        for (int i = 0; i <= 10; i++)
        {
            if (!_groundPool[i].gameObject.activeInHierarchy)
            {
                _groundPool[i].gameObject.SetActive(true);
                return _groundPool[i];
            }
        }
        Ground ground = Instantiate(_ground);
        ++amount;
        ground.gameObject.SetActive(true);
        return ground;
    }
}
