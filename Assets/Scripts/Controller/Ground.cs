using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private SpriteRenderer _normalSprite;
    [SerializeField] private SpriteRenderer _crashSprite;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private bool _isStartGround;
    private int _touchEdgeCount = 0;
    private float _leftEdge;
    private float _rightEdge;
    private int _direction = 1;
    private float _halfWidth;
    private float _time = 0f;
    private float _timeBeforeDestroy = 0.1f;
    private int _touchEdgeLimit = 3;
    private float _disFromEdgeLimit = 0.2f;
    private bool _playerOnTop = false;
    private bool _checkSFX = false;
    private void Start()
    {
        ApplyGround();
        col.size = _normalSprite.sprite.bounds.size;
        col.offset = _normalSprite.sprite.bounds.center;
        _normalSprite.gameObject.SetActive(true);
        _crashSprite.gameObject.SetActive(false);
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        _leftEdge = cam.transform.position.x - camWidth / 2f + _disFromEdgeLimit;
        _rightEdge = cam.transform.position.x + camWidth / 2f - _disFromEdgeLimit;

        _halfWidth = _normalSprite.bounds.size.x / 2f;

        _direction = Random.value < 0.5f ? -1 : 1;
        if(!_isStartGround)
            _moveSpeed += Random.Range(-0.5f, 1.5f);
    }
    private void FixedUpdate()
    {
        if (!_isStartGround)
        {
            int randonDir = Random.value < 0.5f ? 0 : 1;
            if (randonDir == 0)
                MoveGroundNormal();
            else
                MoveGroundNotNormal();
            CheckEdgeAndReverse();
        }
    }
    private void MoveGroundNormal()
    {
        transform.position += new Vector3(_moveSpeed * _direction * Time.deltaTime, 0f, 0f);
    }
    private void MoveGroundNotNormal()
    {
        float _waveFrequency = Random.Range(0f, 2f);
        float _waveAmplitude = Random.Range(0.2f, 0.5f);
        float yOffset = Mathf.Sin(Time.time * _waveFrequency) * _waveAmplitude * Time.deltaTime;
        transform.position += new Vector3(_moveSpeed * _direction * Time.deltaTime, yOffset, 0f);
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
            _normalSprite.gameObject.SetActive(false);
            _crashSprite.gameObject.SetActive(true);
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
        _normalSprite.gameObject.SetActive(true);
        _crashSprite.gameObject.SetActive(false);
        _touchEdgeCount = 0;
        _direction = 1;
        _time = 0f;
        _checkSFX = false;
        _playerOnTop = false;
    }
    public void DisableGround()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag(GameConfig.PLAYER_TAG))
            {
                child.SetParent(null, true);
            }
        }
        // Debug.Log("Ground disable");
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameConfig.PLAYER_TAG))
        {
            _playerOnTop = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameConfig.PLAYER_TAG))
        {
            _playerOnTop = false;
            // DisableGround();
        }
    }
    private void ApplyGround()
    {
        string mapName = PlayerPrefs.GetString(GameConfig.SELECTED_MAP_KEY, "");
        if (string.IsNullOrEmpty(mapName)) return;

        MapData data = Resources.Load<MapData>(GameConfig.MAP_DATA_PATH + mapName);
        if (data != null)
        {
            if (!_isStartGround)
            {
                int randomGround = Random.value < 0.5f ? -1 : 1;
                if (randomGround == -1)
                {
                    _normalSprite.sprite = data.smallNormalGround;
                    _crashSprite.sprite = data.smallCrashGround;
                }
                else
                {
                    _normalSprite.sprite = data.bigNormalGround;
                    _crashSprite.sprite = data.bigCrashGround;
                }
            }
            else
            {
                _normalSprite.sprite = data.smallNormalGround;
                _crashSprite.sprite = data.smallCrashGround;
            }
        }
    }
}
