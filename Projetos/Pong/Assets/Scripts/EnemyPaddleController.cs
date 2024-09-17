using UnityEngine;

public class EnemyPaddleController : MonoBehaviour
{
    public GameSettings settings;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject ball;

    void Update()
    {
        if (ball != null)
        {
            MovePaddle();
        }
    }

    private void MovePaddle()
    {
        float targetY = Mathf.Clamp(ball.transform.position.y, -4.5f, 4.5f); // Limita a posição Y
        Vector2 targetPosition = new Vector2(transform.position.x, targetY);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * settings.difficultyLevel); // Move gradualmente para a posição Y da bola
    }
}
