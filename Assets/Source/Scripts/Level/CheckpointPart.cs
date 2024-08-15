using UnityEngine;

public class CheckpointPart : MapPart
{
    [SerializeField] private UpgradesShopTrigger _upgradesShopTrigger;

    public void Init(UpgradesShop upgradesShop)
    {
        _upgradesShopTrigger.Init(upgradesShop);
    }
}
