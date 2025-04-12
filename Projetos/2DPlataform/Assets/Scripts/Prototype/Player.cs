using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimentação")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 12f;
    private float horizontalInput;

    [Header("Checagem de Solo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    [Header("Componentes")]
    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 originalScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        originalScale = transform.localScale;
    }

    private void Update()
    {
        // Entrada de movimento
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Checagem de solo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Lógica de pulo
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Atualiza parâmetros de animação
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        if (horizontalInput < 0)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        else if (horizontalInput > 0)
            transform.localScale = originalScale;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void UpdateAnimations()
    {
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        animator.SetFloat("YVelocity", rb.velocity.y);
    }

    // Gizmo para visualização do GroundCheck
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
