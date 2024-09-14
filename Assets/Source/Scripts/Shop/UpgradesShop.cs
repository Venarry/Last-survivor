using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesShop : MonoBehaviour
{
    [SerializeField] private GameObject _shopMenu;
    [SerializeField] private DamageForEnemyBuyButton _buyDamageForEnemyButton;
    [SerializeField] private DamageForWoodBuyButton _buyDamageForWoodButton;
    [SerializeField] private DamageForOreBuyButton _buyDamageForOreButton;

    private PricesDataSource _priceDataSource;
    private InventoryModel _inventoryModel;
    private CharacterUpgradesModel<ParametersUpgradeBehaviour> _characterUpgrades;
    private ParameterUpgradesFactory _upgradesFactory;
    private GameTimeScaler _gameTimeScaler;
    private ItemPriceFactory _itemPriceFactory;
    private Dictionary<Type, BuyUpgradeButton> _upgradesButtons;

    private string GameTimeKey => nameof(UpgradesShop);

    public void Init(
        PricesDataSource priceDataSource,
        InventoryModel inventoryModel,
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterUpgrades,
        ParameterUpgradesFactory upgradesFactory,
        ItemPriceFactory itemPriceFactory,
        GameTimeScaler gameTimeScaler)
    {
        _priceDataSource = priceDataSource;
        _inventoryModel = inventoryModel;
        _characterUpgrades = characterUpgrades;
        _upgradesFactory = upgradesFactory;
        _itemPriceFactory = itemPriceFactory;
        _gameTimeScaler = gameTimeScaler;

        _upgradesButtons = new()
        {
            [typeof(DamageForEnemyUpgrade)] = _buyDamageForEnemyButton,
            [typeof(DamageForWoodUpgrade)] = _buyDamageForWoodButton,
            [typeof(DamageForOreUpgrade)] = _buyDamageForOreButton,
        };
    }

    public void Show()
    {
        _shopMenu.SetActive(true);
        _gameTimeScaler.Add(GameTimeKey, timeScale: 0);
    }

    public void Hide()
    {
        _shopMenu.SetActive(false);
        _gameTimeScaler.Remove(GameTimeKey);
    }

    public void InitButtons()
    {
        foreach (Type upgradeType in _upgradesButtons.Keys)
        {
            InitButton(upgradeType, _itemPriceFactory);
        }
    }

    private void InitButton(Type upgradeType, ItemPriceFactory itemPriceFactory, int buyCount = 0)
    {
        Dictionary<LootType, int> basePrice = _priceDataSource.Get(upgradeType);
        _upgradesButtons[upgradeType].Init(_characterUpgrades, _upgradesFactory, _inventoryModel, basePrice, itemPriceFactory, buyCount);
    }
}