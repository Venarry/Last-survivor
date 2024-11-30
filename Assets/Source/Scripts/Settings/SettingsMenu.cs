using General;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _menu;

        private string TimeKey => nameof(SettingsMenu);

        private void Awake()
        {
            _menu.SetActive(false);
        }

        private void OnEnable()
        {
            _settingsButton.onClick.AddListener(OpenMenu);
            _closeButton.onClick.AddListener(CloseMenu);
        }

        private void OnDisable()
        {
            _settingsButton.onClick.RemoveListener(OpenMenu);
            _closeButton.onClick.RemoveListener(CloseMenu);
        }

        private void OpenMenu()
        {
            _menu.SetActive(true);
            GameTimeScaler.Add(TimeKey, 0);
        }

        private void CloseMenu()
        {
            _menu.SetActive(false);
            GameTimeScaler.Remove(TimeKey);
        }
    }
}