using UnityEngine;

[RequireComponent(typeof(HealthBase))]
public class EnemyBase : MonoBehaviour
{
    private Animator animator;
    private IEnemyMovement movementBehavior;
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        movementBehavior = GetComponent<IEnemyMovement>();
        if (movementBehavior != null)
        {
            movementBehavior.Initialize(transform);
        }
        else
        {
            Debug.LogWarning("EnemyBase: Movement behavior not found.", this);
        }
    }

    private void Update()
    {
        movementBehavior?.Update();
    }

    public void PlayAttack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }
    public void PlayDeath()
    {
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
    }
}
