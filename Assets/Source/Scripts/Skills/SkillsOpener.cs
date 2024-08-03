using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillsOpener : MonoBehaviour
{
    private const int SkillsInChoose = 3;

    [SerializeField] private GameObject _skillsParent;
    [SerializeField] private SkillToChoose _skillsPrefab;

    private List<SkillToChoose> _spawnedSkill = new();
    private SkillsSpriteDataSouce _skillsSpriteDataSouce;
    private CharacterSkills _characterSkills;
    private ExperienceModel _experienceModel;
    private SkillsFactory _skillsFactory;

    public void Init(
        SkillsSpriteDataSouce skillsSpriteDataSouce,
        CharacterSkills characterSkills,
        ExperienceModel experienceModel,
        SkillsFactory skillsFactory)
    {
        _skillsSpriteDataSouce = skillsSpriteDataSouce;
        _characterSkills = characterSkills;
        _experienceModel = experienceModel;
        _skillsFactory = skillsFactory;

        _skillsParent.SetActive(false);

        experienceModel.LevelAdd += OnLevelAdd;
    }

    private void OnDestroy()
    {
        _experienceModel.LevelAdd -= OnLevelAdd;
    }

    public void CloseMenu()
    {
        foreach (SkillToChoose spawnedSkill in _spawnedSkill)
        {
            Destroy(spawnedSkill.gameObject);
        }

        _spawnedSkill.Clear();
        Time.timeScale = 1;
    }

    private void OnLevelAdd()
    {
        ISkill[] allSkills = _skillsFactory.CreateAllSkills();
        ISkill[] shuffledSkills = allSkills.OrderBy(c => UnityEngine.Random.Range(0, allSkills.Length)).ToArray();

        int addedSkillsCounter = 0;

        foreach (ISkill skill in shuffledSkills)
        {
            if(_characterSkills.TryGetSkillLevel(skill.GetType(), out int level, out int maxLevel) == true)
            {
                if (level < maxLevel)
                {
                    SpawnSkill(skill, level, maxLevel);
                    addedSkillsCounter++;
                }
            }
            else
            {
                SpawnSkill(skill, skill.CurrentLevel, skill.MaxLevel);
                addedSkillsCounter++;
            }

            if(addedSkillsCounter >= SkillsInChoose)
            {
                break;
            }
        }

        if(addedSkillsCounter != 0)
        {
            _skillsParent.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void SpawnSkill(ISkill skill, int level, int maxLevel)
    {
        Sprite icon = _skillsSpriteDataSouce.Get(skill.GetType());

        SkillToChoose skillButton = Instantiate(_skillsPrefab, _skillsParent.transform);
        skillButton.Init(_characterSkills, this, icon, skill, level, maxLevel);

        _spawnedSkill.Add(skillButton);
    }
}
