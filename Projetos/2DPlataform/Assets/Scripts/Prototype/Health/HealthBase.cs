using System;
using System.Collections;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    [Header("Initial Stats")]
    [SerializeField] private int startHealth;
    [SerializeField] private int _currentHealth;
    private float delayDeath;
    private bool _isDead;
    private bool destroyOnKill;

    [Header("Flash Settings")]
    [SerializeField] private float flashDuration = 0.1f;
    private Color flashColor;
    private Color[] originalColors;

    [Header("Invulnerability Settings")]
    [SerializeField] private bool isInvulnerable = false;
    [SerializeField] private float damageCooldown;
    [SerializeField] private float knockbackForceX;
    [SerializeField] private float knockbackForceY;

    [Header("Componentes")]
    private Rigidbody2D rb;
    private SpriteRenderer[] _spriteRenderer;

    [Header("Events")]
    private Action GetDamaged;

    public int StartHealth { get => startHealth; set => startHealth = value; }
    public int CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public float DelayDeath { get => delayDeath; set => delayDeath = value; }
    public bool IsDead { get => _isDead; set => _isDead = value; }
    public bool DestroyOnKill { get => destroyOnKill; set => destroyOnKill = value; }
    public bool IsInvulnerable { get => isInvulnerable; set => isInvulnerable = value; }
    public SpriteRenderer[] SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }
    public Action GetDamaged1 { get => GetDamaged; set => GetDamaged = value; }
    public Color FlashColor { get => flashColor; set => flashColor = value; }
    public float FlashDuration { get => flashDuration; set => flashDuration = value; }
    public Color[] OriginalColors { get => originalColors; set => originalColors = value; }

    public void Awake()
    {
        SpriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        OriginalColors = new Color[SpriteRenderer.Length];
        for (int i = 0; i < SpriteRenderer.Length; i++)
        {
            OriginalColors[i] = SpriteRenderer[i].color;
        }

        destroyOnKill = true;
        CurrentHealth = StartHealth;
    }

    public void OnEnable()
    {
        GetDamaged1 += HandleGetDamaged;
    }

    public void OnDisable()
    {
        GetDamaged1 -= HandleGetDamaged;
    }

    public Coroutine _flashCoroutine;

    public void HandleGetDamaged()
    {
        Debug.Log("something took damage!");
        FlashSprite();
        Damage(5);

        // Adiciona invulnerabilidade
        IsInvulnerable = true;
        StartCoroutine(InvulnerabilityCoroutine());
    }
    public void Damage(int damage)
    {
        if (IsDead) return;

        CurrentHealth -= damage;
        FlashSprite();

        if (CurrentHealth <= 0) 
        {
            Die();
        }
    }
    public void FlashSprite()
    {
        if (SpriteRenderer != null)
        {
            if (_flashCoroutine != null)
            {
                StopCoroutine(_flashCoroutine);
            }

            _flashCoroutine = StartCoroutine(FlashCoroutine());
        }
    }
    public void Die() 
    {
        IsDead = true;
        if (DestroyOnKill)
        {
            Destroy(gameObject, DelayDeath);
        }
    }
    public void ApplyKnockback(Vector2 direction)
    {
        if (rb == null) return;

        Vector2 force = new Vector2(direction.x * knockbackForceX, knockbackForceY);
        rb.velocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);
    }
    public IEnumerator FlashCoroutine()
    {
        for (int i = 0; i < SpriteRenderer.Length; i++)
        {
            SpriteRenderer[i].color = FlashColor;
        }

        yield return new WaitForSeconds(FlashDuration);

        for (int i = 0; i < SpriteRenderer.Length; i++)
        {
            SpriteRenderer[i].color = OriginalColors[i];
        }
    }
    public IEnumerator InvulnerabilityCoroutine()
    {
        yield return new WaitForSeconds(damageCooldown);
        IsInvulnerable = false;
    }
}
