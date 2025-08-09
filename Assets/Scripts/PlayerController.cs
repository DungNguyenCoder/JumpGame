using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private Ground _ground;
    private Ground _currentGround;
    private bool _canJump = false;
    private bool _onGround = false;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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

        if (playerBottomY < camBottomY)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void HandleTounchInput()
    {
        // if (Input.touchCount > 0)
        // {
        //     Touch touch = Input.GetTouch(0);

        //     if (touch.phase == TouchPhase.Began)
        //     {
        //         Jump();
        //     }
        // }

        if (Application.isEditor && Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    private void Jump()
    {
        Debug.Log("jump");
        if (_canJump)
        {
            _rb.velocity = new Vector2(0f, _jumpForce);
            _canJump = false;
            _onGround = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.AddScore();
        _canJump = true;
        _onGround = true;
        HandleGroundLanding(collision.gameObject);
    }

    private void HandleGroundLanding(GameObject groundObject)
    {
        Debug.Log("ground: " + groundObject.name);
        transform.SetParent(groundObject.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _canJump = false;
            _onGround = false;
            _rb.velocity = new Vector2(0f, _rb.velocity.y);
            transform.SetParent(null);
            Debug.Log(_ground.gameObject.name + " | " + _ground.gameObject.transform.position);
            // _ground.DisableGround();
            collision.gameObject.SetActive(false);
        }
    }
}
