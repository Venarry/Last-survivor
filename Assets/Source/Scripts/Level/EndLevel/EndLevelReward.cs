using UnityEngine;
using UnityEngine.UI;
using YG;

public class EndLevelReward : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private Button _okButton;

    private void Awake()
    {
        _menu.SetActive(false);
    }

    private void OnEnable()
    {
        _okButton.onClick.AddListener(ShowReward);
    }

    public void ShowCongratulationMenu()
    {
        _menu.SetActive(true);
    }

    private void ShowReward()
    {
        YandexGame.FullscreenShow();
        _menu.SetActive(false);
    }
}
