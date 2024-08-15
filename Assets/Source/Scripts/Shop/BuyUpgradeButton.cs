using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyUpgradeButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Transform _priceParent;

    private Dictionary<LootType, ItemPrice> _price = new();
    private ItemPriceFactory _itemPriceFactory;

    public void Init(ItemPriceFactory itemPriceFactory)
    {
        _itemPriceFactory = itemPriceFactory;
    }

    public async void SetPrice(Dictionary<LootType, int> price)
    {
        foreach (KeyValuePair<LootType, int> currentLoot in price)
        {
            if(_price.ContainsKey(currentLoot.Key) == false)
            {
                ItemPrice itemPrice = await _itemPriceFactory.Create(currentLoot.Key, _priceParent);
                _price.Add(currentLoot.Key, itemPrice);
            }

            _price[currentLoot.Key].SetPrice(currentLoot.Value);
        }
    }

    public void AddListenerOnClick(UnityAction call) =>
        _button.onClick.AddListener(call);

    public void RemoveListenerOnClick(UnityAction call) =>
        _button.onClick.RemoveListener(call);
}