using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillsView : MonoBehaviour
{
    private readonly Dictionary<Type, SkillIcon> _skillsIcon = new();
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

    private async void OnSkillAdd(Upgrade skill)
    {
        Type skillType = skill.GetType();

        if(_skillsIcon.ContainsKey(skillType) == false)
        {
            SkillIcon skillIcon = await _skillsViewFactory.CreateSkillIcon(skillType, _skillsParent, skill.CurrentLevel);
            _skillsIcon.Add(skillType, skillIcon);
        }
        else
        {
            _skillsIcon[skillType].Set(skill.CurrentLevel);
        }
    }

    private void OnAllRemoved()
    {
        foreach (KeyValuePair<Type, SkillIcon> icon in _skillsIcon)
        {
            Destroy(icon.Value.gameObject);
        }

        _skillsIcon.Clear();
    }

    private void Update()
    {
        _characterSkillsModel.OnUpdate();
    }
}
