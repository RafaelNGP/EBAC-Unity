using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/Configs/Player Config")]
public class PlayerConfig : ScriptableObject
{
    [Header("Player initial values")]
    [SerializeField] private int initialHP;
    [SerializeField] private float initialSpeed;
    [SerializeField] private float initialJumpForce;

    [Header("Player Attacking values")]
    [SerializeField] private float projectileSpeed;

    [Header("Knockback values")]
    [SerializeField] private float damageCooldown;
    [SerializeField] private float knockbackForceX;
    [SerializeField] private float knockbackForceY;

    public int InitialHP { get => initialHP; set => initialHP = value; }
    public float InitialSpeed { get => initialSpeed; set => initialSpeed = value; }
    public float InitialJumpForce { get => initialJumpForce; set => initialJumpForce = value; }
    public float ProjectileSpeed { get => projectileSpeed; set => projectileSpeed = value; }
    public float DamageCooldown { get => damageCooldown; set => damageCooldown = value; }
    public float KnockbackForceX { get => knockbackForceX; set => knockbackForceX = value; }
    public float KnockbackForceY { get => knockbackForceY; set => knockbackForceY = value; }
}
