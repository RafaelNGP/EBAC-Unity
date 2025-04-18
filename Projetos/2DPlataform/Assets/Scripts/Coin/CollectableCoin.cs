public class CollectableCoin : CollactableBase
{
    public int coinValue = 1;

    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.AddCoins(coinValue);
    }
}