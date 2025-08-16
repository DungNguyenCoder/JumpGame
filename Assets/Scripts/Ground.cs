using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Animator _animator;
    private int _touchEdgeCount = 0;
    private float _leftEdge;
    private float _rightEdge;
    private int _direction = 1;
    private float _halfWidth;
    private float _time = 0f;
    private float _timeBeforeDestroy = 0.1f;
    private int _touchEdgeLimit = 3;
    private float _disFromEdgeLimit = 0.6f;
    private bool _playerOnTop = false;
    private bool _checkSFX = false;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        _leftEdge = cam.transform.position.x - camWidth / 2f + _disFromEdgeLimit;
        _rightEdge = cam.transform.position.x + camWidth / 2f - _disFromEdgeLimit;

        _halfWidth = spriteRenderer.bounds.size.x / 2f;

        _direction = Random.value < 0.5f ? -1 : 1;
    }
    private void FixedUpdate()
    {
        MoveGround();
        CheckEdgeAndReverse();
    }
    private void MoveGround()
    {
        transform.position += new Vector3(_moveSpeed * _direction * Time.deltaTime, 0f, 0f);
    }
    private void CheckEdgeAndReverse()
    {
        float xPos = transform.position.x;

        if (xPos + _halfWidth >= _rightEdge)
        {
            _direction = -1;
            if (_playerOnTop)
                ++_touchEdgeCount;
        }
        else if (xPos - _halfWidth < _leftEdge)
        {
            _direction = 1;
            if (_playerOnTop)
                ++_touchEdgeCount;
        }

        if (_touchEdgeCount == 1 && _checkSFX == false)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.crash);
            _animator.SetTrigger("Crash");
            Debug.Log("Crash");
            _checkSFX = true;
        }

        if (_touchEdgeCount >= _touchEdgeLimit && _playerOnTop)
        {
            _time += Time.deltaTime;
            if (_time >= _timeBeforeDestroy)
            {
                DisableGround();
                _time = 0f;
                _touchEdgeCount = 0;
            }
        }
    }
    private void OnEnable()
    {
        _touchEdgeCount = 0;
        _direction = 1;
        _time = 0f;
    }
    public void DisableGround()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Player"))
            {
                child.SetParent(null, true);
            }
        }
        Debug.Log("Ground disable");
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerOnTop = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerOnTop = false;
            // DisableGround();
        }
    }
}
