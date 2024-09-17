using System.Collections.Generic;
using UnityEngine;

public class UpgradesShop : MonoBehaviour
{
    [SerializeField] private GameObject _shopMenu;
    [SerializeField] private List<BuyUpgradeButton> _buttons;

    private PricesDataSource _priceDataSource;
    private InventoryModel _inventoryModel;
    private CharacterUpgradesModel<ParametersUpgradeBehaviour> _characterUpgrades;
    private ParameterUpgradesFactory _upgradesFactory;
    private GameTimeScaler _gameTimeScaler;
    private ItemPriceFactory _itemPriceFactory;
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
        foreach (BuyUpgradeButton button in _buttons)
        {
            Dictionary<LootType, int> basePrice = _priceDataSource.Get(button.UpgradeType);

            int buyCount = 0;

            if (_buyCountData.ContainsKey(button.UpgradeType))
            {
                buyCount = _buyCountData[button.UpgradeType];
            }

            button.Init(_characterUpgrades, _upgradesFactory, _inventoryModel, basePrice, _itemPriceFactory, buyCount);
        }
    }
}