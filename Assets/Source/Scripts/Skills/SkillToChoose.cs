using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillToChoose : MonoBehaviour
{
    [SerializeField] private Image _skillIcon;
    [SerializeField] private Button _button;
    [SerializeField] private List<Image> _skillUpgradeCountImages;

    [SerializeField] private Image _upgradePrefab;
    [SerializeField] private Transform _upgradesParent;

    [SerializeField] private TMP_Text _skillName;
    [SerializeField] private TMP_Text _skillDescription;
    [SerializeField] private TMP_Text _skillUpgradeDescription;

    private CharacterUpgradesModel<SkillBehaviour> _characterSkills;
    private SkillsOpener _skillOpener;
    private SkillBehaviour _skill;

    public void Init(
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
        SkillsOpener skillsOpener,
        Sprite icon,
        SkillBehaviour skill)
    {
        _characterSkills = characterSkills;
        _skillOpener = skillsOpener;
        _skillIcon.sprite = icon;
        _skill = skill;
    }

    public void SetSkillInformation(
        int currentLevel,
        int maxLevel,
        string name,
        string description,
        string upgradeDescription)
    {
        _skillName.text = name;
        _skillDescription.text = description;
        _skillUpgradeDescription.text = upgradeDescription;

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
