using UnityEngine;

public class EnemyNave : MonoBehaviour
{
    [SerializeField] GameObject shootPoint;
    [SerializeField] PoolManagerEnemies poolManager;
    [SerializeField] StateMachine stateMachine;

    public float minTimeToAttack = 1f; // Tempo mínimo entre ataques
    public float maxTimeToAttack = 3f; // Tempo máximo entre ataques
    private float attackTimer;
    private float timeToNextAttack;

    public float moveSpeed = 1f; // Velocidade do movimento vertical
    public float moveAmplitude = 0.5f; // Amplitude do movimento vertical
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        SetRandomTimeToNextAttack();
    }

    private void Update()
    {
        // Movimento vertical
        float newY = startPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Controle de ataque
        attackTimer += Time.deltaTime;
        if (attackTimer >= timeToNextAttack)
        {
            AttackPlayer();
            attackTimer = 0f;
            SetRandomTimeToNextAttack();
        }
    }

    private void SetRandomTimeToNextAttack()
    {
        timeToNextAttack = Random.Range(minTimeToAttack, maxTimeToAttack);
    }

    public void SpawnObject()
    {
        var obj = poolManager.GetPoolGObjects();
        if (obj != null)
        {
            obj.SetActive(true);
            obj.transform.SetParent(null);
            obj.GetComponent<Projectile>().OnHitPlayer = HitPlayer;
            obj.transform.position = shootPoint.transform.position;
        }
    }
    void HitPlayer()
    {
        stateMachine.SwitchState(StateMachine.States.DEAD);
    }

    void AttackPlayer()
    {
        SpawnObject();
    }
}
