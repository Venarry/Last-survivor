using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillsOpener : MonoBehaviour
{
    [SerializeField] private GameObject _skillsMenu;
    private CharacterSkills _characterSkills;
    private ExperienceModel _experienceModel;
    private SkillsFactory _skillsFactory;

    public void Init(CharacterSkills characterSkills, ExperienceModel experienceModel, SkillsFactory skillsFactory)
    {
        _characterSkills = characterSkills;
        _experienceModel = experienceModel;
        _skillsFactory = skillsFactory;

        experienceModel.LevelAdd += OnLevelAdd;
    }

    private void OnDestroy()
    {
        _experienceModel.LevelAdd -= OnLevelAdd;
    }

    private void OnLevelAdd()
    {
        _skillsMenu.SetActive(true);

        ISkill[] allSkills = _skillsFactory.CreateAllSkills();
        Debug.Log($"allSkills {allSkills.Length}");
        ISkill[] shuffledSkills = allSkills.OrderBy(c => UnityEngine.Random.Range(0, allSkills.Length - 1)).ToArray();
        Debug.Log($"shuffledSkills {shuffledSkills.Length}");

        int addedSkillsCounter = 0;

        foreach (ISkill skill in shuffledSkills)
        {
            if(_characterSkills.TryGetSkillLevel(skill.GetType(), out int level, out int maxLevel) == true)
            {
                if (level < maxLevel)
                {
                    Debug.Log("skill added");
                    addedSkillsCounter++;
                }
            }
            else
            {
                Debug.Log("skill added");
                addedSkillsCounter++;
            }
        }
    }
}
