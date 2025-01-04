using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int startHealth;
    public bool destroyOnKill;
    public float delayDeath;

    private bool _isDead;
    private int _currentHealth;

    private void Awake()
    {
        _currentHealth = startHealth;
    }

    public void Damage(int damage)
    {
        if (_isDead) return;

        _currentHealth -= damage;

        if (_currentHealth <= 0) 
        {
            Die();
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
