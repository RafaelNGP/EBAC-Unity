using UnityEngine;

[CreateAssetMenu(fileName = "SO_EnemySetup", menuName = "Game/Configs/Enemy Config/Enemy1 Config")]
public class Enemy1Config : ScriptableObject
{
    [Header("Health Settings")]
    [SerializeField] private int initialHP;
    [SerializeField] private float delayDeath;
    [SerializeField] private float flashDuration;
    [SerializeField] private Color flashColor;

    [Header("Patrol Settings")]
    [SerializeField] private float patrolDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float waitTime;
    [SerializeField] private float stuckTimeout;

    // 👇 Propriedades públicas somente-leitura
    public int InitialHP => initialHP;
    public float DelayDeath => delayDeath;
    public float FlashDuration => flashDuration;
    public Color FlashColor => flashColor;

    public float PatrolDistance => patrolDistance;
    public float MoveSpeed => moveSpeed;
    public float WaitTime => waitTime;
    public float StuckTimeout => stuckTimeout;
}
