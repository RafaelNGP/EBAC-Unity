using UnityEngine;

public class EnemyPatrolMovement : MonoBehaviour, IEnemyMovement
{
    [SerializeField] private float patrolDistance = 3f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waitTime = 2f;
    [SerializeField] private float stuckTimeout = 2f;
    private float stuckTimer = 0f;

    private Transform enemyTransform;
    private Animator animator;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private Vector3 lastPosition;
    private bool movingRight = true;
    private float waitTimer = 0f;
    private bool waiting = false;

    public void Initialize(Transform enemyTransform)
    {
        this.enemyTransform = enemyTransform;
        animator = enemyTransform.GetComponentInChildren<Animator>();
        initialPosition = enemyTransform.position;
        lastPosition = initialPosition;
        SetTargetPosition();
    }

    public void Update()
    {
        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                movingRight = !movingRight;
                SetTargetPosition();
                waiting = false;
            }
            animator?.SetFloat("Speed", 0f); // Parado
        }
        else
        {
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position, targetPosition, moveSpeed * Time.deltaTime);

            float distance = Vector3.Distance(enemyTransform.position, lastPosition);
            float calculatedSpeed = distance / Time.deltaTime;
            animator?.SetFloat("Speed", calculatedSpeed);

            if (Vector3.Distance(enemyTransform.position, targetPosition) < 0.05f)
            {
                waiting = true;
                waitTimer = 0f;
                stuckTimer = 0f; // reset se chegou corretamente
            }
            else
            {
                stuckTimer += Time.deltaTime;

                if (stuckTimer >= stuckTimeout)
                {
                    Debug.LogWarning("Inimigo ficou preso! Revertendo caminho.");
                    waiting = true;
                    waitTimer = 0f;
                    stuckTimer = 0f;
                    movingRight = !movingRight;
                    SetTargetPosition();
                }
            }

        }

        lastPosition = enemyTransform.position;
    }

    private void SetTargetPosition()
    {
        float direction = movingRight ? 1f : -1f;
        targetPosition = initialPosition + Vector3.right * patrolDistance * direction;

        Vector3 scale = enemyTransform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        enemyTransform.localScale = scale;
    }
}
