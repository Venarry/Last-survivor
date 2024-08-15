using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSwitcher : MonoBehaviour
{
    [SerializeField] private PanelSwitchConnection[] _panelSwitchConnections;
    [SerializeField] private bool _showFirstPanelOnAwake;

    private PanelSwitchConnection _activePanelConnection;

    private void Awake()
    {
        if(_showFirstPanelOnAwake == true)
        {
            _activePanelConnection = _panelSwitchConnections[0];
            _activePanelConnection.Show();
        }

        foreach (var connection in _panelSwitchConnections)
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

[System.Serializable]
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
