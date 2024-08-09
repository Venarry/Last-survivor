using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadingPanel : MonoBehaviour
{
    [SerializeField] private GameObject _loadPanel;
    [SerializeField] private TMP_Text _loadLabel;
    [SerializeField] private Image _loadBar;

    private float _maxProgress;

    public void SetMaxProgress(float maxProgress)
    {
        if(maxProgress < 0)
        {
            maxProgress = 0;
        }

        _maxProgress = maxProgress;
    }

    public void Show(string text, float progress)
    {
        if(_loadPanel.activeInHierarchy == false)
        {
            _loadPanel.SetActive(true);
        }

        _loadBar.fillAmount = progress / _maxProgress;
        _loadLabel.text = text;
    }

    public void Disable()
    {
        _loadPanel.SetActive(false);
    }
}
