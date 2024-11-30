using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class PanelSwitchConnection
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _panel;

        public event Action<PanelSwitchConnection> Clicked;

        public void EnableBehaviour()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        public void Show()
        {
            _panel.SetActive(true);
        }

        public void Hide()
        {
            _panel.SetActive(false);
        }

        private void OnButtonClick()
        {
            Clicked?.Invoke(this);
        }
    }
}