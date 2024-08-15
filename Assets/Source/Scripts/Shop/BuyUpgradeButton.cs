using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class BuyUpgradeButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Transform _priceParent;

    protected CharacterUpgrades CharacterUpgrades;
    protected UpgradesFactory UpgradesFactory;

    private Dictionary<LootType, int> _basePrice;
    private InventoryModel _inventoryModel;
    private readonly Dictionary<LootType, ItemPrice> _priceView = new();
    private ItemPriceFactory _itemPriceFactory;
    private int _buyCount;

    public void Init(
        CharacterUpgrades characterUpgrades,
        UpgradesFactory upgradesFactory,
        InventoryModel inventoryModel,
        Dictionary<LootType, int> basePrice,
        ItemPriceFactory itemPriceFactory)
    {
        CharacterUpgrades = characterUpgrades;
        UpgradesFactory = upgradesFactory;
        _inventoryModel = inventoryModel;
        _itemPriceFactory = itemPriceFactory;
        _basePrice = basePrice;

        SetPrice(GetActualPrice());
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    protected virtual void OnButtonClick()
    {
        Dictionary<LootType, int> targetPrice = GetActualPrice();

        if (TryRemoveCharacterItems(targetPrice) == false)
            return;

        OnUpgradeBuy();
        _buyCount++;
        SetPrice(GetActualPrice());
    }

    protected Dictionary<LootType, int> GetActualPrice()
    {
        Dictionary<LootType, int> targetPrice = new();

        foreach (KeyValuePair<LootType, int> baseLootPrice in _basePrice)
        {
            targetPrice.Add(baseLootPrice.Key, (int)Mathf.Pow(baseLootPrice.Value, _buyCount));
        }

        return targetPrice;
    }

    protected async void SetPrice(Dictionary<LootType, int> price)
    {
        foreach (KeyValuePair<LootType, int> currentLoot in price)
        {
            if (_priceView.ContainsKey(currentLoot.Key) == false)
            {
                ItemPrice itemPrice = await _itemPriceFactory.Create(currentLoot.Key, _priceParent);
                _priceView.Add(currentLoot.Key, itemPrice);
            }

            _priceView[currentLoot.Key].SetPrice(currentLoot.Value);
        }
    }

    protected bool TryRemoveCharacterItems(Dictionary<LootType, int> price) => _inventoryModel.TryRemove(price);

    protected abstract void OnUpgradeBuy();
}