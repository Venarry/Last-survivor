using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LeaderBoardShower : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private Transform _leadersParent;
    [SerializeField] private Button _showMenuButton;
    [SerializeField] private Button _closeMenuButton;

    private void OnEnable()
    {
        _showMenuButton.onClick.AddListener(ShowMenu);
        _closeMenuButton.onClick.AddListener(CloseMenu);
    }

    private void OnDisable()
    {
        _showMenuButton.onClick.RemoveListener(ShowMenu);
        _closeMenuButton.onClick.RemoveListener(CloseMenu);
    }

    private void ShowMenu()
    {
        _menu.SetActive(true);

        YandexGame.GetLeaderboard(GameParameters.LeaderboardName, 100, 10, 5, "small");
    }

    private void CloseMenu()
    {
        _menu.SetActive(false);
    }
}
