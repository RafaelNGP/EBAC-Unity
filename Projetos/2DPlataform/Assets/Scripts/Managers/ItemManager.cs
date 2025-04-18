using System;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance; 
    public static event Action<int> OnCoinChanged;
    public static event Action<int> OnGenChanged;
    public int coins;
    public int gems;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins = 0;
        gems = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
        OnCoinChanged?.Invoke(coins);
    }

    public void GotGem(int amount = 1)
    {
        gems++;
        OnGenChanged?.Invoke(gems);
    }
}
