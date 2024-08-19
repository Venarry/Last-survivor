using UnityEngine;
using UnityEngine.UI;

public class CloseShopButton : MonoBehaviour
{
    [SerializeField] private UpgradesShop _shop;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        _shop.Hide();
    }
}
