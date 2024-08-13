using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _countLabel;

    public void Init(Sprite icon, int count = 0)
    {
        _icon.sprite = icon;
        _countLabel.text = count.ToString();
    }

    public void SetCount(int count)
    {
        _countLabel.text = count.ToString();
    }
}
