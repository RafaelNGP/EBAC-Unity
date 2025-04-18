public class CollectableGem : CollactableBase
{
    protected override void OnCollect()
    {
        base.OnCollect();
        ItemManager.Instance.GotGem();
    }
}