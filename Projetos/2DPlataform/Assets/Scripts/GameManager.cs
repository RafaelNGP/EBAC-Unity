using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    [SerializeField] private GameObject player;

    [Header("References")]
    [SerializeField] private Transform spawnPoint;

    [Header("Enemies")]
    [SerializeField] private List<GameObject> enemies;

    GameObject _currentPlayer;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        _currentPlayer = Instantiate(player);
        _currentPlayer.transform.position = spawnPoint.position;
    }
}
