using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesShop : MonoBehaviour
{
    [SerializeField] private GameObject _shopMenu;
    [SerializeField] private BuyUpgradeButton _buyDamageForEnemyButton;
    [SerializeField] private BuyUpgradeButton _buyDamageForWoodButton;
    [SerializeField] private BuyUpgradeButton _buyDamageForOreButton;

    private InventoryModel _inventoryModel;
    private CharacterUpgrades _characterUpgrades;
    private UpgradesFactory _upgradesFactory;

    private readonly Dictionary<Type, Dictionary<LootType, int>> _upgradesPrice = new()
    {
        [typeof(DamageForEnemyUpgrade)] = new()
        {
            [LootType.Wood] = 1,
        },

        [typeof(DamageForWoodUpgrade)] = new()
        {
            [LootType.Wood] = 1,
        },

        [typeof(DamageForOreUpgrade)] = new()
        {
            [LootType.Diamond] = 1,
            [LootType.Wood] = 1,
        },
    };

    public void Init(
        InventoryModel inventoryModel,
        CharacterUpgrades characterUpgrades,
        UpgradesFactory upgradesFactory,
        ItemPriceFactory itemPriceFactory)
    {
        _inventoryModel = inventoryModel;
        _characterUpgrades = characterUpgrades;
        _upgradesFactory = upgradesFactory;

        _buyDamageForEnemyButton.Init(itemPriceFactory);
        _buyDamageForEnemyButton.SetPrice(_upgradesPrice[typeof(DamageForEnemyUpgrade)]);

        _buyDamageForWoodButton.Init(itemPriceFactory);
        _buyDamageForWoodButton.SetPrice(_upgradesPrice[typeof(DamageForWoodUpgrade)]);

        _buyDamageForOreButton.Init(itemPriceFactory);
        _buyDamageForOreButton.SetPrice(_upgradesPrice[typeof(DamageForOreUpgrade)]);
    }

    private void OnEnable()
    {
        _buyDamageForEnemyButton.AddListenerOnClick(OnClickBuyDamageForEnemy);
        _buyDamageForWoodButton.AddListenerOnClick(OnClickBuyDamageForWood);
        _buyDamageForOreButton.AddListenerOnClick(OnClickBuyDamageForOre);
    }

    private void OnDisable()
    {
        _buyDamageForEnemyButton.RemoveListenerOnClick(OnClickBuyDamageForEnemy);
        _buyDamageForWoodButton.RemoveListenerOnClick(OnClickBuyDamageForWood);
        _buyDamageForOreButton.RemoveListenerOnClick(OnClickBuyDamageForOre);
    }

    private void OnClickBuyDamageForOre()
    {
        TryBuy(_upgradesPrice[typeof(DamageForOreUpgrade)], _upgradesFactory.CreateDamageForOre);
    }

    private void OnClickBuyDamageForWood()
    {
        TryBuy(_upgradesPrice[typeof(DamageForWoodUpgrade)], _upgradesFactory.CreateDamageForWood);
    }

    private void OnClickBuyDamageForEnemy()
    {
        TryBuy(_upgradesPrice[typeof(DamageForEnemyUpgrade)], _upgradesFactory.CreateDamageForEnemy);
    }

    private void TryBuy(Dictionary<LootType, int> price, Func<UpgradeBehaviour> buy)
    {
        if (_inventoryModel.TryRemove(price) == false)
            return;

        _characterUpgrades.Add(buy.Invoke());
    }
}