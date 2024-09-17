using System.Collections.Generic;
using UnityEngine;

public class ProgressHandler : IProgressSaveService
{
    private const string SaveName = "Save";

    private readonly InventoryModel _inventoryModel;
    private readonly HealthModel _healthModel;
    private readonly ExperienceModel _experienceModel;
    private readonly LevelsStatisticModel _levelsStatisticModel;
    private readonly CharacterUpgradesModel<ParametersUpgradeBehaviour> _characterUpgrades;
    private readonly CharacterUpgradesModel<SkillBehaviour> _characterSkills;
    private readonly SkillsFactory _skillsFactory;
    private readonly ParameterUpgradesFactory _parameterUpgradesFactory;
    private readonly UpgradesShop _upgradesShop;
    private ProgressData _data;

    public ProgressHandler(
        InventoryModel inventoryModel,
        HealthModel healthModel,
        ExperienceModel experienceModel,
        LevelsStatisticModel levelsStatisticModel,
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterUpgrades,
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
        SkillsFactory skillsFactory,
        ParameterUpgradesFactory parameterUpgradesFactory,
        UpgradesShop upgradesShop)
    {
        _inventoryModel = inventoryModel;
        _healthModel = healthModel;
        _experienceModel = experienceModel;
        _levelsStatisticModel = levelsStatisticModel;
        _characterUpgrades = characterUpgrades;
        _characterSkills = characterSkills;
        _skillsFactory = skillsFactory;
        _parameterUpgradesFactory = parameterUpgradesFactory;
        _upgradesShop = upgradesShop;
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(SaveName) == true)
        {
            _data = JsonUtility.FromJson<ProgressData>(PlayerPrefs.GetString(SaveName));
            InjectData();
        }
        else
        {
            _data = new();
        }
    }

    public void Save()
    {
        _data.HealthNormalized = _healthModel.HealthNormalized;
        _data.SetLevels(_levelsStatisticModel.TotalLevel);
        _data.SetExperienceData(_experienceModel.CurrentLevel, _experienceModel.CurrentExperience);

        foreach (KeyValuePair<LootType, int> loot in _inventoryModel.GetAll())
        {
            _data.SetLoot(loot.Key, loot.Value);
        }

        foreach (ParametersUpgradeBehaviour upgrade in _characterUpgrades.GetAll())
        {
            _data.AddUpgrade(upgrade.UpgradeType, upgrade.CurrentLevel);
        }

        foreach (SkillBehaviour skill in _characterSkills.GetAll())
        {
            _data.AddSkill(skill.UpgradeType, skill.CurrentLevel);
        }

        string data = JsonUtility.ToJson(_data);

        PlayerPrefs.SetString(SaveName, data);
        Debug.Log(PlayerPrefs.GetString(SaveName));
    }

    private void InjectData()
    {
        LoadLoot();
        LoadSkills();
        LoadUpgrades();

        _upgradesShop.Load(_data.Upgrades.ToArray());
        _levelsStatisticModel.Set(_data.TotalLevels);
        _healthModel.SetNormalizedHealth(_data.HealthNormalized);
        _experienceModel.Load(_data.ExperienceData.Level, _data.ExperienceData.Experience);
    }

    private void LoadLoot()
    {
        foreach (LootData lootData in _data.Loots)
        {
            _inventoryModel.Add(lootData.LootType, lootData.Count);
        }
    }

    private void LoadSkills()
    {
        List<SkillBehaviour> skills = new();

        foreach (UpgradeData upgrade in _data.Skills)
        {
            SkillBehaviour skill = _skillsFactory.CreateBy(upgrade.Type, upgrade.Level);
            skills.Add(skill);
        }

        _characterSkills.Load(skills.ToArray());
    }

    private void LoadUpgrades()
    {
        List<ParametersUpgradeBehaviour> upgrades = new();

        foreach (UpgradeData upgradeData in _data.Upgrades)
        {
            ParametersUpgradeBehaviour upgrade = _parameterUpgradesFactory.CreateBy(upgradeData.Type, upgradeData.Level);
            upgrades.Add(upgrade);
        }

        _characterUpgrades.Load(upgrades.ToArray());
    }
}
