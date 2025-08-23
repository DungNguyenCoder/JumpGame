using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GroundPool : MonoBehaviour
{
    [SerializeField] private Ground _groundPrefabs;

    private int amount = 10;
    private List<Ground> _groundPool = new List<Ground>();

    private void Awake()
    {
        InitPool();
    }

    private void InitPool()
    {
        for (int i = 0; i < amount; i++)
        {
            Ground ground = Instantiate(_groundPrefabs);
            ground.name = "" + (i + 1);
            ground.gameObject.SetActive(false);
            _groundPool.Add(ground);
            Debug.Log("Init Ground");
        }
    }

    public Ground GetGround()
    {
        foreach (Ground ground in _groundPool)
        {
            if (!ground.gameObject.activeInHierarchy)
            {
                ground.gameObject.SetActive(true);
                return ground;
            }
        }
        Ground newGround = Instantiate(_groundPrefabs);
        ++amount;
        newGround.gameObject.SetActive(true);
        return newGround;
    }

    public void ReturnPool(Ground ground)
    {
        ground.gameObject.SetActive(false);
    }
}
