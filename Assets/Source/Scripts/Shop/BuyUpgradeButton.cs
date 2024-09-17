using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class BuyUpgradeButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Transform _priceParent;
    [SerializeField] private TMP_Text _upgradeDescription;

    protected CharacterUpgradesModel<ParametersUpgradeBehaviour> CharacterUpgrades;
    protected ParameterUpgradesFactory UpgradesFactory;

    private ParametersUpgradeBehaviour _upgrade; 
    private Dictionary<LootType, int> _basePrice;
    private InventoryModel _inventoryModel;
    private readonly Dictionary<LootType, ItemPriceView> _priceView = new();
    private ItemPriceFactory _itemPriceFactory;
    private int _buyCount;

    public void Init(
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterUpgrades,
        ParameterUpgradesFactory upgradesFactory,
        InventoryModel inventoryModel,
        Dictionary<LootType, int> basePrice,
        ItemPriceFactory itemPriceFactory,
        int buyCount = 0)
    {
        CharacterUpgrades = characterUpgrades;
        UpgradesFactory = upgradesFactory;
        _inventoryModel = inventoryModel;
        _itemPriceFactory = itemPriceFactory;
        _basePrice = basePrice;
        _buyCount = buyCount;

        InitUpgrade();
        SetPriceView(GetActualPrice());
        SetDescription();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void InitUpgrade()
    {
        _upgrade = CreateUpgrade();
        CharacterUpgrades.AddWithoutIncreaseLevel(_upgrade);
    }

    protected abstract ParametersUpgradeBehaviour CreateUpgrade();

    protected virtual void OnButtonClick()
    {
        Dictionary<LootType, int> targetPrice = GetActualPrice();

        if (TryRemoveCharacterItems(targetPrice) == false)
            return;

        OnUpgradeBuy();
        _buyCount++;
        SetPriceView(GetActualPrice());
        SetDescription();
    }

    private Dictionary<LootType, int> GetActualPrice()
    {
        Dictionary<LootType, int> targetPrice = new();

        foreach (KeyValuePair<LootType, int> baseLootPrice in _basePrice)
        {
            int minLootCount = 2;
            int lootCount = Mathf.Max(baseLootPrice.Value, minLootCount);

            float defaultLootPrice = lootCount * (_buyCount + 1);
            int lootPrice = (int)Mathf.Ceil(defaultLootPrice + Mathf.Pow(defaultLootPrice, 1.4f) * GameParamenters.PriceMultiplier);

            targetPrice.Add(baseLootPrice.Key, lootPrice);
        }

        return targetPrice;
    }

    private async void SetPriceView(Dictionary<LootType, int> price)
    {
        foreach (KeyValuePair<LootType, int> currentLoot in price)
        {
            if (_priceView.ContainsKey(currentLoot.Key) == false)
            {
                ItemPriceView itemPrice = await _itemPriceFactory.Create(currentLoot.Key, _priceParent);
                _priceView.Add(currentLoot.Key, itemPrice);
            }

            _priceView[currentLoot.Key].SetPrice(currentLoot.Value);
        }
    }

    private void SetDescription()
    {
        Type upgradeType = _upgrade.GetType();

        if(CharacterUpgrades.TryGetUpLevelDescription(upgradeType, out string description))
        {
            _upgradeDescription.text = description;
        }
        else
        {
            _upgradeDescription.text = "Error";
        }
    }

    protected bool TryRemoveCharacterItems(Dictionary<LootType, int> price) =>
        _inventoryModel.TryRemove(price);

    private void OnUpgradeBuy()
    {
        _upgrade.TryIncreaseLevel();
    }
}