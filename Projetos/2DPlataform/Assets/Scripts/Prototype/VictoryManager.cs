using System;
using System.Collections.Generic;
using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public event Action OnVictory;
    public event Action OnDefeat;

    [SerializeField] GameObject canvasVictory;
    [SerializeField] GameObject canvasDefeat;
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        OnDefeat += Defeat;
        OnVictory += Victory;
    }

    private void OnDisable()
    {
        OnDefeat -= Defeat;
        OnVictory -= Victory;
    }

    private void Update()
    {
        CleanUpEnemies();
        CheckVictory();
        CheckDefeat();
    }

    private void CleanUpEnemies()
    {
        gameManager.GetEnemies().RemoveAll(enemy => enemy == null);
    }

    private void CheckVictory()
    {
        if (gameManager.GetEnemies().Count == 0)
        {
            OnVictory?.Invoke();
        }
    }

    private void CheckDefeat()
    {
        if (gameManager.GetPlayer() == null)
        {
            OnDefeat?.Invoke();
        }
    }

    private void Defeat()
    {
        canvasDefeat.SetActive(true);
        canvasVictory.SetActive(false);
    }

    private void Victory()
    {
        canvasVictory.SetActive(true);
        canvasDefeat.SetActive(false);
    }
}
