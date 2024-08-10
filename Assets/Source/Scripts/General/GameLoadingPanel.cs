using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadingPanel : MonoBehaviour
{
    [SerializeField] private GameObject _loadPanel;
    [SerializeField] private TMP_Text _loadLabel;
    [SerializeField] private Image _loadBar;

    private int _currentProgress;
    private string[] _labels;

    public void Set(string[] labels)
    {
        _currentProgress = 0;
        _labels = labels.ToArray();
    }

    public void ShowNext()
    {
        if (_labels.Length == 0)
            return;

        if(_loadPanel.activeInHierarchy == false)
        {
            _loadPanel.SetActive(true);
        }

        _loadBar.fillAmount = (float)_currentProgress / _labels.Length;
        _loadLabel.text = _labels[_currentProgress];

        if(_currentProgress < _labels.Length - 1)
        {
            _currentProgress++;
        }
    }

    public void Disable()
    {
        _loadPanel.SetActive(false);
    }
}
