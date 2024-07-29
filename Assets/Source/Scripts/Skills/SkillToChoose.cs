using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillToChoose : MonoBehaviour
{
    [SerializeField] private Image _skillIcon;
    [SerializeField] private Button _button;

    private CharacterSkills _characterSkills;
    private SkillsOpener _skillOpener;
    private ISkill _skill;

    public void Init(
        CharacterSkills characterSkills,
        SkillsOpener skillsOpener,
        Sprite icon,
        ISkill skill)
    {
        _characterSkills = characterSkills;
        _skillOpener = skillsOpener;
        _skillIcon.sprite = icon;
        _skill = skill;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _characterSkills.Add(_skill);
        _skillOpener.CloseMenu();
    }
}
