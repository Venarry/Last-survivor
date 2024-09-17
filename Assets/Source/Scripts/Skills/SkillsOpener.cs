using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillsOpener : MonoBehaviour
{
    [SerializeField] private GameObject _skillsParent;
    [SerializeField] private SkillToChoose _skillsPrefab;

    private List<SkillToChoose> _spawnedSkill = new();
    private SkillsViewFactory _skillToChooseFactory;
    private CharacterUpgradesModel<SkillBehaviour> _characterSkills;
    private ExperienceModel _experienceModel;
    private SkillsFactory _skillsFactory;
    private GameTimeScaler _gameTimeScaler;
    private int _levelsInQueue = 0;

    private string GameTimeKey => nameof(SkillsOpener);

    public void Init(
        SkillsViewFactory skillToChooseFactory,
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
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

    public void CloseMenuAndRemoveSkills()
    {
        foreach (SkillToChoose spawnedSkill in _spawnedSkill)
        {
            Destroy(spawnedSkill.gameObject);
        }

        _spawnedSkill.Clear();
        _gameTimeScaler.Remove(GameTimeKey);

        if(_levelsInQueue > 0)
        {
            _levelsInQueue--;
            OnLevelAdd();
        }
    }

    private void OnLevelAdd()
    {
        if(_spawnedSkill.Count != 0)
        {
            _levelsInQueue++;
            return;
        }

        SkillBehaviour[] allSkills = _skillsFactory.CreateAllSkills();
        SkillBehaviour[] shuffledSkills = allSkills.OrderBy(c => UnityEngine.Random.Range(0, allSkills.Length)).ToArray();

        int addedSkillsCounter = 0;

        foreach (SkillBehaviour skill in shuffledSkills)
        {
            Type skillType = skill.GetType();

            if (_characterSkills.HasUpgrade(skillType))
            {
                _characterSkills.TryGetUpgradeLevel(skillType, out int level, out int maxLevel);
                _characterSkills.TryGetUpLevelDescription(skillType, out string upgradeDescription);
                
                if (level < maxLevel)
                {
                    SpawnSkill(skill, level, maxLevel, upgradeDescription);
                    addedSkillsCounter++;
                }
            }
            else
            {
                SpawnSkill(skill, skill.CurrentLevel, skill.MaxLevel, skill.GetUpLevelDescription());
                addedSkillsCounter++;
            }

            if(addedSkillsCounter >= GameParamenters.SkillsToChooseByLevel)
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

    private async void SpawnSkill(SkillBehaviour skill, int level, int maxLevel, string upgradeDescription)
    {
        SkillToChoose skillButton = await _skillToChooseFactory
            .CreateSkillButton(_skillsParent.transform, _characterSkills, this, skill, level, maxLevel, upgradeDescription);

        _spawnedSkill.Add(skillButton);
    }
}
