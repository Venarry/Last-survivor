using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelIcon : MonoBehaviour
{
    private const float ActiveSize = 1.3f;

    [SerializeField] private Image _outline;
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private Color _activeColor;

    private Color _deactiveColor;
    private Vector3 _activeScale = new(ActiveSize, ActiveSize, ActiveSize);
    private Vector3 _deactiveScale = Vector3.one;

    private void Awake()
    {
        _deactiveColor = _outline.color;
    }

    public void SetLevelNumber(int number)
    {
        _levelNumber.text = number.ToString();
    }

    public void SetActiveColor()
    {
        _outline.color = _activeColor;
    }

    public void SetActiveSize()
    {
        transform.localScale = _activeScale;
    }

    public void SetDectiveColor()
    {
        _outline.color = _deactiveColor;
    }

    public void SetDectiveSize()
    {
        transform.localScale = _deactiveScale;
    }
}
