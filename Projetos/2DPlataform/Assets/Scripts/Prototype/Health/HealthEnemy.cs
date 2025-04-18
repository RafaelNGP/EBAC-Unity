using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : HealthBase
{
    [SerializeField] private Enemy1Config enemyConfig;

    private new void Awake()
    {
        StartHealth = enemyConfig.InitialHP;
        FlashColor = enemyConfig.FlashColor;
        base.Awake();
    }
}

