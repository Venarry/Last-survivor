using System.Collections.Generic;
using Configs;
using General;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Utils.LB;

namespace YSDK
{
    public class LeaderBoardShower : MonoBehaviour
    {
        private readonly List<UserLeaderScoreView> _userLeaderScoreViews = new ();

        [Header("Leaderboard")]
        [SerializeField] private GameObject _menu;
        [SerializeField] private Transform _leadersParent;
        [SerializeField] private Button _showMenuButton;
        [SerializeField] private Button _closeMenuButton;
        [SerializeField] private UserLeaderScoreView _userLeaderScoreView;
        [SerializeField] private UserLeaderScoreView _thisPlayer;

        [Header("Warning menu")]
        [SerializeField] private GameObject _warningMenu;
        [SerializeField] private Button _authButton;
        [SerializeField] private Button _closeWarningMenuButton;

        private string TimeKey => nameof(LeaderBoardShower);

        private void Awake()
        {
            _warningMenu.SetActive(false);
            _menu.SetActive(false);
        }

        private void OnEnable()
        {
            _showMenuButton.onClick.AddListener(ShowMenu);
            _authButton.onClick.AddListener(Auth);
            _closeMenuButton.onClick.AddListener(CloseLeadersMenu);
            _closeWarningMenuButton.onClick.AddListener(CloseWarningMenu);

            YandexGame.onGetLeaderboard += OnLeaderboardGet;
        }

        private void OnDisable()
        {
            _showMenuButton.onClick.RemoveListener(ShowMenu);
            _authButton.onClick.RemoveListener(Auth);
            _closeMenuButton.onClick.RemoveListener(CloseLeadersMenu);
            _closeWarningMenuButton.onClick.RemoveListener(CloseWarningMenu);

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

            _thisPlayer.Set(data.thisPlayer.rank, YandexGame.playerName, data.thisPlayer.score);
        }

        private void ShowMenu()
        {
            if (YandexGame.auth == true)
            {
                _menu.SetActive(true);

                YandexGame.GetLeaderboard(
                    GameParameters.LeaderboardName,
                    maxQuantityPlayers: 100,
                    quantityTop: 10,
                    quantityAround: 5,
                    photoSizeLB: "small");
            }
            else
            {
                _warningMenu.SetActive(true);
            }

            GameTimeScaler.Add(TimeKey, 0f);
        }

        private void CloseLeadersMenu()
        {
            _menu.SetActive(false);
            GameTimeScaler.Remove(TimeKey);
        }

        private void CloseWarningMenu()
        {
            _warningMenu.SetActive(false);
            GameTimeScaler.Remove(TimeKey);
        }

        private void Auth()
        {
            CloseWarningMenu();
            YandexGame.AuthDialog();
        }
    }
}