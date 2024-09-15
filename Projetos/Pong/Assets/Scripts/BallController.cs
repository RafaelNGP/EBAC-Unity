using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameManager gameManager;
    private Rigidbody2D rb;
    private Vector2 startingVelocity = new Vector2(5f, 5f);
    private bool canCollide = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Desativa a gravidade
        rb.velocity = startingVelocity;
    }

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        rb.velocity = startingVelocity;
        canCollide = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canCollide) return;

        Debug.Log("Collision with: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("TopWall"))
        {
            Debug.Log("Hit Wall");
            Vector2 newVelocity = rb.velocity;
            newVelocity.y = -newVelocity.y;
            rb.velocity = newVelocity;
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit Paddle");
            Vector2 newVelocity = rb.velocity;
            newVelocity.x = -newVelocity.x;
            rb.velocity = newVelocity;
        }

        if (collision.gameObject.CompareTag("WallEnemy"))
        {
            Debug.Log("Hit WallEnemy");
            gameManager.ScorePlayer();
            ResetBall();
        }
        else if (collision.gameObject.CompareTag("WallPlayer"))
        {
            Debug.Log("Hit WallPlayer");
            gameManager.ScoreEnemy();
            ResetBall();
        }

        StartCoroutine(CollisionCooldown());
    }

    private IEnumerator CollisionCooldown()
    {
        canCollide = false;
        yield return new WaitForSeconds(0.1f); // Ajuste o tempo conforme necessário
        canCollide = true;
    }
}
