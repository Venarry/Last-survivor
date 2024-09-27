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
    private readonly CharacterUpgradesModel<ParametersUpgradeBehaviour> _characterPrestigeUpgrades;
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
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterPrestigeUpgrades,
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
        _characterPrestigeUpgrades = characterPrestigeUpgrades;
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
        _data.ResetUpgrades();
        _data.ResetPrestigeUpgrades();
        _data.ResetSkills();

        foreach (KeyValuePair<LootType, int> loot in _inventoryModel.GetAll())
        {
            _data.SetLoot(loot.Key, loot.Value);
        }

        foreach (ParametersUpgradeBehaviour upgrade in _characterUpgrades.GetAll())
        {
            _data.AddUpgrade(upgrade.UpgradeType, upgrade.CurrentLevel);
        }

        foreach (ParametersUpgradeBehaviour upgrade in _characterPrestigeUpgrades.GetAll())
        {
            _data.AddPrestigeUpgrade(upgrade.UpgradeType, upgrade.CurrentLevel);
        }

        foreach (SkillBehaviour skill in _characterSkills.GetAll())
        {
            _data.AddSkill(skill.UpgradeType, skill.CurrentLevel);
        }

        string data = JsonUtility.ToJson(_data);

        PlayerPrefs.SetString(SaveName, data);
        Debug.Log(PlayerPrefs.GetString(SaveName));
    }

    public void ReloadShop()
    {
        _upgradesShop.ReloadButtons(_data.Upgrades.ToArray());
    }

    private void InjectData()
    {
        LoadLoot();
        LoadSkills();
        LoadUpgrades(_characterUpgrades, _data.Upgrades);
        LoadUpgrades(_characterPrestigeUpgrades, _data.PrestigeUpgrades);

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

    private void LoadUpgrades(CharacterUpgradesModel<ParametersUpgradeBehaviour> characterUpgradesModel, List<UpgradeData> source)
    {
        List<ParametersUpgradeBehaviour> upgrades = new();

        foreach (UpgradeData upgradeData in source)
        {
            ParametersUpgradeBehaviour upgrade = _parameterUpgradesFactory.CreateBy(upgradeData.Type, upgradeData.Level);
            upgrades.Add(upgrade);
        }

        characterUpgradesModel.Load(upgrades.ToArray());
    }
}
