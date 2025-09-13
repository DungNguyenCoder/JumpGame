using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private GroundSpawner _groundSpawner;
    [SerializeField] private Ground _ground;
    [SerializeField] private GameObject playerAnimation;

    private Ground _currentGround;
    private bool _canJump = false;
    private bool _onGround = false;
    private float _perfectDis = 0.1f;
    private bool _isOver = false;
    private Rigidbody2D _rb;
    private Collider2D _col;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
        ApplySelectedSkin();
    }
    private void Update()
    {
        HandleTounchInput();
    }
    private void LateUpdate()
    {
        CheckFallOutOfCam();
    }
    private void CheckFallOutOfCam()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camBottomY = cam.transform.position.y - camHeight / 2f;

        float playerBottomY = transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2f;

        if (playerBottomY < camBottomY && !_isOver)
        {
            GameManager.Instance.GameOver();
            _isOver = true;
        }
    }

    private void HandleTounchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (UIManager.Instance.IsPointerOverUI())
                {
                    // Debug.Log("Click on UI");
                    return;
                }
                Jump();
            }
        }
        else if (Application.isEditor && Input.GetMouseButtonDown(0))
        {
            // Debug.Log("Jump");
            if (UIManager.Instance.IsPointerOverUI())
            {
                // Debug.Log("Click on UI");
                return;
            }
            Jump();
        }
    }

    private void Jump()
    {
        if (_canJump)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
            _rb.velocity = new Vector2(0f, _jumpForce);
            _canJump = false;
            _onGround = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameConfig.START_GROUND_TAG))
        {
            // Debug.Log("Landed on start ground");
            _canJump = true;
            _onGround = true;
            HandleGroundLanding(collision.gameObject);
            return;
        }
        Vector3 groundCol = collision.collider.bounds.center;
        float disX = Mathf.Abs(_col.bounds.center.x - groundCol.x);
        if (disX < _perfectDis)
        {
            GameManager.Instance.AddScore(2);
            InGameUI.Instance.UpdateScore();
            GameManager.Instance.SetPerfect(true);
            InGameUI.Instance.StartPerfectTime();
            // Debug.Log("Perfect");
        }
        else
        {
            GameManager.Instance.AddScore(1);
            InGameUI.Instance.UpdateScore();
        }
        _canJump = true;
        _onGround = true;
        HandleGroundLanding(collision.gameObject);
        Ground landedGround = collision.gameObject.GetComponent<Ground>();
        if (landedGround != null && _groundSpawner != null)
            _groundSpawner.PlayerLanded(landedGround);
    }

    private void HandleGroundLanding(GameObject groundObject)
    {
        transform.SetParent(groundObject.transform, true);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GameConfig.GROUND_TAG) || collision.gameObject.CompareTag(GameConfig.START_GROUND_TAG))
        {
            _canJump = false;
            _onGround = false;
            _rb.velocity = new Vector2(0f, _rb.velocity.y);
            if (transform.parent == collision.transform)
                transform.SetParent(null, true);
            collision.gameObject.SetActive(false);
        }
    }

    private void ApplySelectedSkin()
    {
        playerAnimation = Instantiate(playerAnimation, transform);
        string charName = PlayerPrefs.GetString(GameConfig.SELECTED_CHARACTER_KEY, "");
        if (string.IsNullOrEmpty(charName))
        {
            // Debug.Log("No data");
            return;
        }

        CharacterData data = Resources.Load<CharacterData>(GameConfig.CHARACTER_DATA_PATH + charName);
        if (data != null)
        {
            if (data.playerAnimation != null)
            {
                // Debug.Log("Load animator success");
                playerAnimation.SetActive(false);
                playerAnimation = Instantiate(data.playerAnimation, this.transform);
                return;
            }
            else
            {
                // Debug.LogWarning("Animator missing");
            }
        }
        
    }
}
