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
    private CharacterUpgrades _characterUpgrades;
    private UpgradesFactory _upgradesFactory;

    private readonly Dictionary<Type, Dictionary<LootType, int>> _upgradesPrice = new()
    {
        [typeof(DamageForEnemyUpgrade)] = new()
        {
            [LootType.Wood] = 2,
        },

        [typeof(DamageForWoodUpgrade)] = new()
        {
            [LootType.Wood] = 2,
        },

        [typeof(DamageForOreUpgrade)] = new()
        {
            [LootType.Diamond] = 2,
            [LootType.Wood] = 2,
        },
    };

    private Dictionary<Type, BuyUpgradeButton> _upgradesButtons;

    public void Init(
        InventoryModel inventoryModel,
        CharacterUpgrades characterUpgrades,
        UpgradesFactory upgradesFactory,
        ItemPriceFactory itemPriceFactory)
    {
        _inventoryModel = inventoryModel;
        _characterUpgrades = characterUpgrades;
        _upgradesFactory = upgradesFactory;

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
    }

    public void Hide()
    {
        _shopMenu.SetActive(false);
    }

    private void InitButton(Type upgradeType, ItemPriceFactory itemPriceFactory)
    {
        Dictionary<LootType, int> basePrice = _upgradesPrice[upgradeType];
        _upgradesButtons[upgradeType].Init(_characterUpgrades, _upgradesFactory, _inventoryModel, basePrice, itemPriceFactory);
    }
}