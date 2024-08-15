using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPrice : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _price;

    public void SetIcon(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    public void SetPrice(int price)
    {
        _price.text = price.ToString();
    }
}