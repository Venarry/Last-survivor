using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

public class LeaderBoardShower : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private Transform _leadersParent;
    [SerializeField] private Button _showMenuButton;
    [SerializeField] private Button _closeMenuButton;
    [SerializeField] private UserLeaderScoreView _userLeaderScoreView;

    private List<UserLeaderScoreView> _userLeaderScoreViews = new();

    private void Awake()
    {
        _menu.SetActive(false);
    }

    private void OnEnable()
    {
        _showMenuButton.onClick.AddListener(ShowMenu);
        _closeMenuButton.onClick.AddListener(CloseMenu);

        YandexGame.onGetLeaderboard += OnLeaderboardGet;
    }

    private void OnDisable()
    {
        _showMenuButton.onClick.RemoveListener(ShowMenu);
        _closeMenuButton.onClick.RemoveListener(CloseMenu);

        YandexGame.onGetLeaderboard -= OnLeaderboardGet;
    }

    private void OnLeaderboardGet(LBData data)
    {
        foreach (UserLeaderScoreView user in _userLeaderScoreViews)
        {
            Destroy(user.gameObject);
        }

        _userLeaderScoreViews.Clear();

        foreach (LBPlayerData playerData in data.players)
        {
            UserLeaderScoreView playerView = Instantiate(_userLeaderScoreView, _leadersParent);
            playerView.Set(playerData.rank, playerData.name, playerData.score);

            _userLeaderScoreViews.Add(playerView);
        }
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