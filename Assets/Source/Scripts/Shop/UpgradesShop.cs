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
    private Dictionary<UpgradeType, BuyUpgradeButton> _upgradesButtons;
    private readonly Dictionary<UpgradeType, int> _buyCountData = new();

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
            [UpgradeType.DamageForEnemy] = _buyDamageForEnemyButton,
            [UpgradeType.DamageForWood] = _buyDamageForWoodButton,
            [UpgradeType.DamageForOre] = _buyDamageForOreButton,
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

    public void Load(UpgradeData[] upgrades)
    {
        foreach (UpgradeData upgradeData in upgrades)
        {
            _buyCountData.Add(upgradeData.Type, upgradeData.Level);
        }
    }

    public void InitButtons()
    {
        /*foreach (Type upgradeType in _upgradesButtons.Keys)
        {
            InitButton(upgradeType, _itemPriceFactory);
        }*/

        foreach (KeyValuePair<UpgradeType, BuyUpgradeButton> upgradeButton in _upgradesButtons)
        {
            Dictionary<LootType, int> basePrice = _priceDataSource.Get(upgradeButton.Key);
            int buyCount = 0;

            if (_buyCountData.ContainsKey(upgradeButton.Key))
            {
                buyCount = _buyCountData[upgradeButton.Key];
            }

            upgradeButton.Value.Init(_characterUpgrades, _upgradesFactory, _inventoryModel, basePrice, _itemPriceFactory, buyCount);
        }
    }

    /*private void InitButton(Type upgradeType, ItemPriceFactory itemPriceFactory, int buyCount = 0)
    {
        Dictionary<LootType, int> basePrice = _priceDataSource.Get(upgradeType);
        _upgradesButtons[upgradeType].Init(_characterUpgrades, _upgradesFactory, _inventoryModel, basePrice, itemPriceFactory, buyCount);
    }*/
}