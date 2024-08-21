using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillsOpener : MonoBehaviour
{
    private const int SkillsInChoose = 3;

    [SerializeField] private GameObject _skillsParent;
    [SerializeField] private SkillToChoose _skillsPrefab;

    private List<SkillToChoose> _spawnedSkill = new();
    private SkillsViewFactory _skillToChooseFactory;
    private CharacterSkillsModel _characterSkills;
    private ExperienceModel _experienceModel;
    private SkillsFactory _skillsFactory;
    private GameTimeScaler _gameTimeScaler;

    private string GameTimeKey => nameof(SkillsOpener);

    public void Init(
        SkillsViewFactory skillToChooseFactory,
        CharacterSkillsModel characterSkills,
        ExperienceModel experienceModel,
        SkillsFactory skillsFactory,
        GameTimeScaler gameTimeScaler)
    {
        _skillToChooseFactory = skillToChooseFactory;
        _characterSkills = characterSkills;
        _experienceModel = experienceModel;
        _skillsFactory = skillsFactory;
        _gameTimeScaler = gameTimeScaler;

        _skillsParent.SetActive(false);

        experienceModel.LevelAdded += OnLevelAdd;
    }

    private void OnDestroy()
    {
        _experienceModel.LevelAdded -= OnLevelAdd;
    }

    public void CloseMenu()
    {
        foreach (SkillToChoose spawnedSkill in _spawnedSkill)
        {
            Destroy(spawnedSkill.gameObject);
        }

        _spawnedSkill.Clear();
        _gameTimeScaler.Remove(GameTimeKey);
    }

    private void OnLevelAdd()
    {
        ISkill[] allSkills = _skillsFactory.CreateAllSkills();
        ISkill[] shuffledSkills = allSkills.OrderBy(c => UnityEngine.Random.Range(0, allSkills.Length)).ToArray();

        int addedSkillsCounter = 0;

        foreach (ISkill skill in shuffledSkills)
        {
            Type skillType = skill.GetType();

            if (_characterSkills.HasSkill(skillType))
            {
                _characterSkills.TryGetSkillLevel(skillType, out int level, out int maxLevel);
                string upgradeDescription = _characterSkills.GetSkillUpgradeDescription(skillType);
                
                if (level < maxLevel)
                {
                    SpawnSkill(skill, level, maxLevel, upgradeDescription);
                    addedSkillsCounter++;
                }
            }
            else
            {
                SpawnSkill(skill, skill.CurrentLevel, skill.MaxLevel, skill.GetUpgradeDescription());
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
            _gameTimeScaler.Add(GameTimeKey, timeScale: 0);
        }
    }

    private async void SpawnSkill(ISkill skill, int level, int maxLevel, string upgradeDescription)
    {
        SkillToChoose skillButton = await _skillToChooseFactory
            .CreateSkillButton(_skillsParent.transform, _characterSkills, this, skill, level, maxLevel, upgradeDescription);

        _spawnedSkill.Add(skillButton);
    }
}
