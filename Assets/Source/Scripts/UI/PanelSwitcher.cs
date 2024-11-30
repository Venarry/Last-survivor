using UnityEngine;

namespace UI
{
    public class PanelSwitcher : MonoBehaviour
    {
        [SerializeField] private PanelSwitchConnection[] _panelSwitchConnections;
        [SerializeField] private bool _showFirstPanelOnAwake;

        private PanelSwitchConnection _activePanelConnection;

        private void Awake()
        {
            if (_showFirstPanelOnAwake == true)
            {
                _activePanelConnection = _panelSwitchConnections[0];
                _activePanelConnection.Show();
            }

            foreach (PanelSwitchConnection connection in _panelSwitchConnections)
            {
                connection.EnableBehaviour();
                connection.Clicked += OnSwitchButtonClick;
            }
        }

        private void OnSwitchButtonClick(PanelSwitchConnection panel)
        {
            _activePanelConnection?.Hide();
            _activePanelConnection = panel;
            _activePanelConnection.Show();
        }
    }
}