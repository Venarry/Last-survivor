using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillsView : MonoBehaviour
{
    private readonly List<SkillIcon> _skillIcons = new();
    private CharacterUpgradesModel<SkillBehaviour> _characterSkillsModel;
    private SkillsViewFactory _skillsViewFactory;
    private Transform _skillsParent;

    public void Init(CharacterUpgradesModel<SkillBehaviour> characterSkillsModel, SkillsViewFactory skillsViewFactory, Transform parent)
    {
        _characterSkillsModel = characterSkillsModel;
        _skillsViewFactory = skillsViewFactory;
        _skillsParent = parent;

        _characterSkillsModel.Added += OnSkillAdd;
        _characterSkillsModel.AllRemoved += OnAllRemoved;
    }

    private void OnDestroy()
    {
        _characterSkillsModel.Added -= OnSkillAdd;
        _characterSkillsModel.AllRemoved -= OnAllRemoved;
    }

    private async void OnSkillAdd(IUpgrade skill)
    {
        SkillIcon skillIcon = await _skillsViewFactory.CreateSkillIcon(skill.GetType(), _skillsParent);
        _skillIcons.Add(skillIcon);
    }

    private void OnAllRemoved()
    {
        foreach (SkillIcon icon in _skillIcons)
        {
            Destroy(icon.gameObject);
        }

        _skillIcons.Clear();
    }

    private void Update()
    {
        _characterSkillsModel.OnUpdate();
    }
}
