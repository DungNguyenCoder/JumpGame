using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _followSpeed = 5f;
    private Vector3 _targetPosition;
    private bool _shouldFollow = false;
    public void MoveToY(float y)
    {
        _targetPosition = new Vector3(transform.position.x, y, transform.position.z);
        _shouldFollow = true;
    }

    private void Update()
    {
        if (_shouldFollow)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, Time.deltaTime * _followSpeed);

            if (Mathf.Abs(transform.position.y - _targetPosition.y) < 0.01f)
            {
                transform.position = _targetPosition;
                _shouldFollow = false;
            }
        }
    }
}
