using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField] private Image _icon;

    public void Set(Sprite sprite)
    {
        _icon.sprite = sprite;
    }
}
