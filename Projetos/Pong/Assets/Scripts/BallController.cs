using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speedIncrement = 0.5f;
    [SerializeField] float maxSpeed = 20f;
    float increment;
    public float testSpeed;
    private Vector2 startingVelocity = new Vector2(5f, 5f);
    private bool canCollide = true;

    private void Start()
    {
        rb.velocity = startingVelocity;
        increment = speedIncrement;
    }

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        rb.velocity = startingVelocity;
        increment = speedIncrement;
        canCollide = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canCollide) return;

        //Detect if collision with a paddle
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 newVelocity = rb.velocity;
            newVelocity.x = -newVelocity.x + increment;
            increment = -increment;

            if (newVelocity.x <= -maxSpeed) newVelocity.x = -maxSpeed;
            if (newVelocity.x >= maxSpeed) newVelocity.x = maxSpeed;
            
            rb.velocity = newVelocity;
            testSpeed = newVelocity.x;
        }

        //Detect if collision with upper or under wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 newVelocity = rb.velocity;
            newVelocity.y = -newVelocity.y;
            rb.velocity = newVelocity;
        }

        // Detect if Player Scored 
        if (collision.gameObject.CompareTag("WallEnemy"))
        {
            gameManager.ScorePlayer();
            ResetBall();
        }
        
        // Detect if Enemy scored
        if (collision.gameObject.CompareTag("WallPlayer"))
        {
            gameManager.ScoreEnemy();
            ResetBall();
        }

        StartCoroutine(CollisionCooldown());
    }

    private IEnumerator CollisionCooldown()
    {
        canCollide = false;
        yield return new WaitForSeconds(0.05f); // Ajuste o tempo conforme necessário
        canCollide = true;
    }
}
