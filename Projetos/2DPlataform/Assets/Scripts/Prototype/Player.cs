using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Configuração")]
    [SerializeField] private PlayerConfig playerConfig;
    private HealthBase health;

    [Header("Movimentação")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float horizontalInput;

    [Header("Checagem de Solo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    [Header("Attacking")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;

    [Header("Getting Damaged")]
    private bool isInvulnerable = false;
    [SerializeField] private float damageCooldown;
    [SerializeField] private float knockbackForceX;
    [SerializeField] private float knockbackForceY;

    [Header("Componentes")]
    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 originalScale;

    [Header("Events")]
    private Action GetDamaged;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<HealthBase>();
        rb = GetComponent<Rigidbody2D>();
        GetDamaged += HandleGetDamaged;

        moveSpeed = playerConfig.InitialSpeed;
        jumpForce = playerConfig.InitialJumpForce;
        projectileSpeed = playerConfig.ProjectileSpeed;
        damageCooldown = playerConfig.DamageCooldown;
        knockbackForceX = playerConfig.KnockbackForceX;
        knockbackForceY = playerConfig.KnockbackForceY;

        originalScale = transform.localScale;
        health.startHealth = playerConfig.InitialHP;
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

        // Lógica de ataque
        HandleAttack();

        // Atualiza parâmetros de animação
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisão com: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isInvulnerable)
            {
                Debug.Log("Ignorado por invulnerabilidade!");
                return;
            }

            Vector2 hitDirection = (transform.position - collision.transform.position).normalized;
            ApplyKnockback(hitDirection);
            GetDamaged?.Invoke();
        }
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
    private void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Attacking");

            GameObject projectile = ObjectPooling.Instance.GetObject();
            projectile.transform.SetPositionAndRotation(firePoint.position, firePoint.rotation);
            projectile.SetActive(true);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.right * projectileSpeed;

            if (transform.localScale.x < 0)
            {
                rb.velocity = -firePoint.right * projectileSpeed;
            }
        }
    }
    private void HandleGetDamaged()
    {
        Debug.Log("Player took damage!");
        health.FlashSprite();
        health.Damage(5);

        // Adiciona invulnerabilidade
        isInvulnerable = true;
        StartCoroutine(InvulnerabilityCoroutine());
    }
    private IEnumerator InvulnerabilityCoroutine()
    {
        yield return new WaitForSeconds(damageCooldown);
        isInvulnerable = false;
    }
    private void ApplyKnockback(Vector2 direction)
    {
        if (rb == null) return;

        Vector2 force = new Vector2(direction.x * knockbackForceX, knockbackForceY);
        rb.velocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
