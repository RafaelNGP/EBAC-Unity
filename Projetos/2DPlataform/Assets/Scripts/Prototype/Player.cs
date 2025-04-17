using System;
using System.Collections;
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

    [Header("Attacking")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;

    [Header("Getting Damaged")]
    private bool isInvulnerable = false;
    [SerializeField] private float damageCooldown = 1f;
    [SerializeField] private float knockbackForceX = 5f;
    [SerializeField] private float knockbackForceY = 3f;


    [Header("Componentes")]
    private Rigidbody2D rb;
    private Animator animator;
    private Vector3 originalScale;

    [Header("Events")]
    private Action GetDamaged;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GetDamaged += HandleGetDamaged;
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
        HealthBase health = GetComponent<HealthBase>();
        health.FlashSprite();
        health.Damage(5);
        ApplyKnockback();

        // Adiciona invulnerabilidade
        isInvulnerable = true;
        StartCoroutine(InvulnerabilityCoroutine());
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        yield return new WaitForSeconds(damageCooldown);
        isInvulnerable = false;
    }

    private void ApplyKnockback()
    {
        if (rb == null) return;

        float horizontalForce = transform.localScale.x > 0 ? -knockbackForceX : knockbackForceX;
        Vector2 force = new Vector2(horizontalForce, knockbackForceY);

        rb.velocity = Vector2.zero; // zera a velocidade atual pra evitar empurrões cumulativos
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
