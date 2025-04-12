using TMPro;
using UnityEngine;

public class ShotAttack : MonoBehaviour
{
    [SerializeField] private float projetilSpeed = .1f;
    [SerializeField] private int bulletDamage = 10;

    void Awake()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * projetilSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<HealthBase>().Damage(bulletDamage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }

    public int BulletDamage
    {
        get { return bulletDamage; }
        set { bulletDamage = value; }
    }
}
