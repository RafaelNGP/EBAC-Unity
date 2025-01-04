using TMPro;
using UnityEngine;

public class ShotAttack : MonoBehaviour
{   
    public float projetilSpeed = .1f;

    void Awake()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * projetilSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<HealthBase>().Die();

        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
