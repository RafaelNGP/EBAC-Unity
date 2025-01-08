using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCoin : CollactableBase
{
    public int coinValue = 1;

    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddCoins(coinValue);
    }
}


