using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    [SerializeField] private GameObject player;

    [Header("Enemies")]
    [SerializeField] private List<GameObject> enemies;

    public List<GameObject> GetEnemies()
    {
        return enemies;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
