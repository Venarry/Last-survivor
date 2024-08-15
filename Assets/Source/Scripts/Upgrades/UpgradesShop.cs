using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesShop : MonoBehaviour
{
    [SerializeField] private GameObject _shopMenu;
    [SerializeField] private Button _buyDamageForEnemyButton;
    [SerializeField] private Button _buyDamageForWoodButton;
    [SerializeField] private Button _buyDamageForOreButton;

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
        UpgradesFactory upgradesFactory)
    {
        _inventoryModel = inventoryModel;
        _characterUpgrades = characterUpgrades;
        _upgradesFactory = upgradesFactory;
    }

    private void OnEnable()
    {
        _buyDamageForEnemyButton.onClick.AddListener(OnClickBuyDamageForEnemy);
        _buyDamageForWoodButton.onClick.AddListener(OnClickBuyDamageForWood);
        _buyDamageForOreButton.onClick.AddListener(OnClickBuyDamageForOre);
    }

    private void OnDisable()
    {
        _buyDamageForEnemyButton.onClick.RemoveListener(OnClickBuyDamageForEnemy);
        _buyDamageForWoodButton.onClick.RemoveListener(OnClickBuyDamageForWood);
        _buyDamageForOreButton.onClick.RemoveListener(OnClickBuyDamageForOre);
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