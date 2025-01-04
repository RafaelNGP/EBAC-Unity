using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Movement Setup")]
    public Rigidbody2D rigidbody2;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 2;

    private float _currentSpeed;
    private bool _isRunning;

    [Header("Animation Setup")]
    public float startJumpY = 1.5f;
    public float startJumpX = .7f;
    public float endJumpY = .7f;
    public float endJumpX = 1.5f;

    private bool _isJumping;

    private float jumpDuration = .3f;

    [Header("Attack Setup")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firepointLeft;
    [SerializeField] Transform firepointRight;
    private bool _isMovingLeft;
    private bool _isMovingRight;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAttack();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2.velocity = Vector2.up * forceJump;
            HandleAnimation();
        }
    }
    private void HandleAnimation()
    {
        rigidbody2.transform.localScale = Vector2.one;
        DOTween.Kill(rigidbody2.transform);
        rigidbody2.transform.DOScaleY(startJumpY, jumpDuration).SetLoops(2, LoopType.Yoyo);
        rigidbody2.transform.DOScaleX(startJumpX, jumpDuration).SetLoops(2, LoopType.Yoyo);
        _isJumping = true;
    }
    private void HandleMovement()
    {
        _isRunning = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody2.velocity = new Vector2(_isRunning ? -speedRun : -speed, rigidbody2.velocity.y);
            _isMovingLeft = true;
            _isMovingRight = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody2.velocity = new Vector2(_isRunning ? speedRun : speed, rigidbody2.velocity.y);
            _isMovingLeft = false;
            _isMovingRight = true;
        }

        if (rigidbody2.velocity.x > 0)
        {
            rigidbody2.velocity += friction;
        }
        else if (rigidbody2.velocity.x < 0)
        {
            rigidbody2.velocity -= friction;
        }
    }

    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(projectile, 
                _isMovingLeft ? firepointLeft.position : firepointRight.position, 
                _isMovingLeft ? firepointLeft.rotation : firepointRight.rotation);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            if (_isJumping)
            {
                DOTween.Kill(rigidbody2.transform);
                rigidbody2.transform.DOScaleY(endJumpY, jumpDuration).SetLoops(2, LoopType.Yoyo);
                rigidbody2.transform.DOScaleX(endJumpX, jumpDuration).SetLoops(2, LoopType.Yoyo);
                _isJumping = false;
            }
        }
    }

}
