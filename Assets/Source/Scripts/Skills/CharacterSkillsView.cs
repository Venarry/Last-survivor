using UnityEngine;

public class CharacterSkillsView : MonoBehaviour
{
    private CharacterSkillsModel _characterSkillsModel;
    private SkillsViewFactory _skillsViewFactory;
    private Transform _skillsParent;

    public void Init(CharacterSkillsModel characterSkillsModel, SkillsViewFactory skillsViewFactory, Transform parent)
    {
        _characterSkillsModel = characterSkillsModel;
        _skillsViewFactory = skillsViewFactory;
        _skillsParent = parent;

        _characterSkillsModel.Added += OnSkillAdd;
    }

    private void OnDestroy()
    {
        _characterSkillsModel.Added -= OnSkillAdd;
    }

    private void OnSkillAdd(ISkill skill)
    {
        _skillsViewFactory.CreateSkillIcon(skill.GetType(), _skillsParent);
    }

    private void Update()
    {
        _characterSkillsModel.OnUpdate();
    }
}
