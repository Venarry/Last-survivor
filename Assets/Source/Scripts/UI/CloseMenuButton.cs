using UnityEngine;
using UnityEngine.UI;

public class CloseMenuButton : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

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
        _menu.SetActive(false);
    }
}
