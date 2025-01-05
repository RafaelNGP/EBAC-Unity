using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Movement Setup")]
    public Rigidbody2D rigidbody2;
    public Vector2 friction = new(.1f, 0);
    public Vector2 playerSizeL = new(-.5f, .5f);
    public Vector2 playerSizeR = new(.5f, .5f);
    public float speed;
    public float speedRun;
    public float forceJump = 2;

    private bool _isRunning;
    private bool _isWalkingRight;

    [Header("Animation Setup")]
    public float startJumpY;
    public float startJumpX;
    public float endJumpY;
    public float endJumpX;

    [SerializeField] private Animator animator;

    private bool _isJumping;
    private float jumpDuration = .3f;

    [Header("Attack Setup")]
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firepoint;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAttack();
    }
    private void HandleMovement()
    {
        _isRunning = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("Walking", true);
            gameObject.transform.localScale = playerSizeL;
            firepoint.rotation = Quaternion.Euler(0, 0, -180);
            rigidbody2.velocity = new Vector2(_isRunning ? -speedRun : -speed, rigidbody2.velocity.y);
            _isWalkingRight = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("Walking", true);
            gameObject.transform.localScale = playerSizeR;
            firepoint.rotation = Quaternion.Euler(0, 0, 0);
            rigidbody2.velocity = new Vector2(_isRunning ? speedRun : speed, rigidbody2.velocity.y);
            _isWalkingRight = true;
        }
        else animator.SetBool("Walking", false);

        if (rigidbody2.velocity.x > 0) rigidbody2.velocity += friction;
        else if (rigidbody2.velocity.x < 0) rigidbody2.velocity -= friction;
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
        DOTween.Kill(rigidbody2.transform);
        rigidbody2.transform.DOScaleY(startJumpY, jumpDuration).SetLoops(2, LoopType.Yoyo);
        rigidbody2.transform.DOScaleX(_isWalkingRight ? startJumpX : -startJumpX, jumpDuration).SetLoops(2, LoopType.Yoyo);
        _isJumping = true;
    }

    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("Attacking");
            Instantiate(projectile, firepoint.position, firepoint.rotation);
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
                rigidbody2.transform.DOScaleX(_isWalkingRight ? endJumpX : -endJumpX, jumpDuration).SetLoops(2, LoopType.Yoyo);
                _isJumping = false;
            }
        }
    }

}
