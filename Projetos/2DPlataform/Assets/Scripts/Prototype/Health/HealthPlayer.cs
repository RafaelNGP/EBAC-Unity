using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : HealthBase
{
    [SerializeField] private PlayerConfig playerConfig;

    private new void Awake()
    {
        StartHealth = playerConfig.InitialHP;
        FlashColor = playerConfig.FlashColor;
        base.Awake();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisão com: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (IsInvulnerable)
            {
                Debug.Log("Ignorado por invulnerabilidade!");
                return;
            }

            Vector2 hitDirection = (transform.position - collision.transform.position).normalized;
            ApplyKnockback(hitDirection);
            GetDamaged1?.Invoke();
        }
    }
}
