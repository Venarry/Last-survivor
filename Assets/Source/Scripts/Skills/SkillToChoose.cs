using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillToChoose : MonoBehaviour
{
    [SerializeField] private Image _skillIcon;
    [SerializeField] private Button _button;
    [SerializeField] private List<Image> _skillUpgradeCountImages;

    [SerializeField] private Image _upgradePrefab;
    [SerializeField] private Transform _upgradesParent;

    private IUpgradable<ISkill> _characterSkills;
    private SkillsOpener _skillOpener;
    private ISkill _skill;

    public void Init(
        IUpgradable<ISkill> characterSkills,
        SkillsOpener skillsOpener,
        Sprite icon,
        ISkill skill, 
        int currentLevel,
        int maxLevel)
    {
        _characterSkills = characterSkills;
        _skillOpener = skillsOpener;
        _skillIcon.sprite = icon;
        _skill = skill;

        for (int i = 0; i < maxLevel; i++)
        {
            Image image = Instantiate(_upgradePrefab, _upgradesParent);

            image.color = i < currentLevel ? Color.yellow : Color.black;
        }
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
