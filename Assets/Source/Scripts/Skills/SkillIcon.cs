using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _level;

    public void Set(Sprite sprite)
    {
        _icon.sprite = sprite;
    }

    public void Set(int level)
    {
        _level.text = level.ToString();
    }
}
