using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesShop : MonoBehaviour, ITutorialAction
{
    [SerializeField] private GameObject _shopMenu;
    [SerializeField] private List<BuyUpgradeButton> _buttons;
    [SerializeField] private List<BuyUpgradeButton> _prestigeButtons;

    private PricesDataSource _priceDataSource;
    private InventoryModel _inventoryModel;
    private CharacterUpgradesModel<ParametersUpgradeBehaviour> _characterUpgrades;
    private CharacterUpgradesModel<ParametersUpgradeBehaviour> _characterPrestigeUpgrades;
    private ParameterUpgradesFactory _upgradesFactory;
    private GameTimeScaler _gameTimeScaler;
    private ItemPriceFactory _itemPriceFactory;
    private Dictionary<UpgradeType, int> _buyCountData = new();

    public event Action<ITutorialAction> Happened;

    private string GameTimeKey => nameof(UpgradesShop);

    public void Init(
        PricesDataSource priceDataSource,
        InventoryModel inventoryModel,
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterUpgrades,
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterPrestigeUpgrades,
        ParameterUpgradesFactory upgradesFactory,
        ItemPriceFactory itemPriceFactory,
        GameTimeScaler gameTimeScaler)
    {
        _priceDataSource = priceDataSource;
        _inventoryModel = inventoryModel;
        _characterUpgrades = characterUpgrades;
        _characterPrestigeUpgrades = characterPrestigeUpgrades;
        _upgradesFactory = upgradesFactory;
        _itemPriceFactory = itemPriceFactory;
        _gameTimeScaler = gameTimeScaler;

        Hide();
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

    public void ReloadButtons(UpgradeData[] upgrades)
    {
        Load(upgrades);
        ResetButtonsBuyCount();
    }

    public void Load(UpgradeData[] upgrades)
    {
        _buyCountData = new();

        foreach (UpgradeData upgradeData in upgrades)
        {
            _buyCountData.Add(upgradeData.Type, upgradeData.Level);
        }
    }

    public void InitButtons()
    {
        foreach (BuyUpgradeButton button in _buttons)
        {
            InitButton(button, _characterUpgrades);
        }

        foreach (BuyUpgradeButton button in _prestigeButtons)
        {
            InitButton(button, _characterPrestigeUpgrades);
        }
    }

    private void InitButton(BuyUpgradeButton button, CharacterUpgradesModel<ParametersUpgradeBehaviour> characterUpgrades)
    {
        Dictionary<LootType, int> basePrice = _priceDataSource.Get(button.UpgradeType);

        int buyCount = GetBuyCount(button.UpgradeType);

        button.Init(characterUpgrades, _upgradesFactory, _inventoryModel, basePrice, _itemPriceFactory, buyCount);
        button.AddListener(OnAnyButtonClick);
    }

    private void OnAnyButtonClick()
    {
        Happened?.Invoke(this);

        foreach (BuyUpgradeButton button in _buttons)
        {
            button.RemoveListener(OnAnyButtonClick);
        }

        foreach (BuyUpgradeButton button in _prestigeButtons)
        {
            button.RemoveListener(OnAnyButtonClick);
        }
    }

    private void ResetButtonsBuyCount()
    {
        ResetButtonsBuyCount(_buttons);
        ResetButtonsBuyCount(_prestigeButtons);
    }

    private void ResetButtonsBuyCount(List<BuyUpgradeButton> buttons)
    {
        foreach (BuyUpgradeButton button in buttons)
        {
            int buyCount = GetBuyCount(button.UpgradeType);
            button.ReloadButton(buyCount);
        }
    }

    private int GetBuyCount(UpgradeType upgradeType)
    {
        int buyCount = 0;

        if (_buyCountData.ContainsKey(upgradeType))
        {
            buyCount = _buyCountData[upgradeType];
        }

        return buyCount;
    }
}