using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    private int damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            var health = collision.transform.GetComponent<HealthBase>();
            if (health != null)
            {
                damage = collision.gameObject.GetComponent<ShotAttack>().BulletDamage;
                health.Damage(damage);
            }
        }
    }
}
