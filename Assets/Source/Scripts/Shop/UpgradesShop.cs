using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradesShop : MonoBehaviour
{
    [SerializeField] private GameObject _shopMenu;
    [SerializeField] private DamageForEnemyBuyButton _buyDamageForEnemyButton;
    [SerializeField] private DamageForWoodBuyButton _buyDamageForWoodButton;
    [SerializeField] private DamageForOreBuyButton _buyDamageForOreButton;

    private InventoryModel _inventoryModel;
    private CharacterUpgradesModel<ParametersUpgradeBehaviour> _characterUpgrades;
    private ParameterUpgradesFactory _upgradesFactory;
    private GameTimeScaler _gameTimeScaler;

    private string GameTimeKey => nameof(UpgradesShop);

    private readonly Dictionary<Type, Dictionary<LootType, int>> _upgradesPrice = new()
    {
        [typeof(DamageForEnemyUpgrade)] = new()
        {
            [LootType.Wood] = 4,
        },

        [typeof(DamageForWoodUpgrade)] = new()
        {
            [LootType.Wood] = 4,
        },

        [typeof(DamageForOreUpgrade)] = new()
        {
            [LootType.Diamond] = 2,
            [LootType.Wood] = 4,
        },
    };

    private Dictionary<Type, BuyUpgradeButton> _upgradesButtons;

    public void Init(
        InventoryModel inventoryModel,
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterUpgrades,
        ParameterUpgradesFactory upgradesFactory,
        ItemPriceFactory itemPriceFactory,
        GameTimeScaler gameTimeScaler)
    {
        _inventoryModel = inventoryModel;
        _characterUpgrades = characterUpgrades;
        _upgradesFactory = upgradesFactory;
        _gameTimeScaler = gameTimeScaler;

        _upgradesButtons = new()
        {
            [typeof(DamageForEnemyUpgrade)] = _buyDamageForEnemyButton,
            [typeof(DamageForWoodUpgrade)] = _buyDamageForWoodButton,
            [typeof(DamageForOreUpgrade)] = _buyDamageForOreButton,
        };

        foreach (Type upgradeType in _upgradesButtons.Keys)
        {
            InitButton(upgradeType, itemPriceFactory);
        }
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

    private void InitButton(Type upgradeType, ItemPriceFactory itemPriceFactory)
    {
        Dictionary<LootType, int> basePrice = _upgradesPrice[upgradeType].ToDictionary(x => x.Key, x => x.Value);
        _upgradesButtons[upgradeType].Init(_characterUpgrades, _upgradesFactory, _inventoryModel, basePrice, itemPriceFactory);
    }
}