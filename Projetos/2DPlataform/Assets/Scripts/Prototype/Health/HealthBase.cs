using System.Collections;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int startHealth;
    public bool destroyOnKill;
    public float delayDeath;

    private bool _isDead;
    private int _currentHealth;

    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashDuration = 0.1f;

    private Color[] originalColors;
    private SpriteRenderer[] _spriteRenderer;

    private void Awake()
    {
        _currentHealth = startHealth;
        _spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        originalColors = new Color[_spriteRenderer.Length];

        for (int i = 0; i < _spriteRenderer.Length; i++)
        {
            originalColors[i] = _spriteRenderer[i].color;
        }
    }

    public void Damage(int damage)
    {
        if (_isDead) return;

        _currentHealth -= damage;
        FlashSprite();

        if (_currentHealth <= 0) 
        {
            Die();
        }
    }

    private Coroutine _flashCoroutine;

    public void FlashSprite()
    {
        if (_spriteRenderer != null)
        {
            if (_flashCoroutine != null)
            {
                StopCoroutine(_flashCoroutine);
            }

            _flashCoroutine = StartCoroutine(FlashCoroutine());
        }
    }

    private IEnumerator FlashCoroutine()
    {
        for (int i = 0; i < _spriteRenderer.Length; i++)
        {
            _spriteRenderer[i].color = flashColor;
            //Debug.Log($"[FlashCoroutine] {gameObject.name} color changed to {flashColor} on part {i}");
        }

        yield return new WaitForSeconds(flashDuration);

        for (int i = 0; i < _spriteRenderer.Length; i++)
        {
            _spriteRenderer[i].color = originalColors[i];
            //Debug.Log($"[FlashCoroutine] {gameObject.name} color reverted to {originalColors[i]} on part {i}");
        }
    }



    public void Die() 
    {
        _isDead = true;
        if (destroyOnKill)
        {
            Destroy(gameObject, delayDeath);
        }
    }
}
