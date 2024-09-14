using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProgressHandler : IProgressSaveService
{
    private const string SaveName = "Save";

    private readonly InventoryModel _inventoryModel;
    private readonly LevelsStatisticModel _levelsStatisticModel;
    private readonly CharacterUpgradesModel<ParametersUpgradeBehaviour> _characterUpgrades;
    private readonly CharacterUpgradesModel<SkillBehaviour> _characterSkills;
    private readonly SkillsFactory _skillsFactory;
    private readonly ParameterUpgradesFactory _parameterUpgradesFactory;
    private ProgressData _data;

    public ProgressHandler(
        InventoryModel inventoryModel,
        LevelsStatisticModel levelsStatisticModel,
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterUpgrades,
        CharacterUpgradesModel<SkillBehaviour> characterSkills,
        SkillsFactory skillsFactory,
        ParameterUpgradesFactory parameterUpgradesFactory)
    {
        _inventoryModel = inventoryModel;
        _levelsStatisticModel = levelsStatisticModel;
        _characterUpgrades = characterUpgrades;
        _characterSkills = characterSkills;
        _skillsFactory = skillsFactory;
        _parameterUpgradesFactory = parameterUpgradesFactory;

        _inventoryModel.ItemChanged += OnItemChange;
        _levelsStatisticModel.Added += OnLevelChange;

        _characterUpgrades.Added += OnUpgradeAdd;
        _characterUpgrades.AllRemoved += OnUpgradesRemove;

        _characterSkills.Added += OnSkillAdd;
        _characterSkills.AllRemoved += OnSkillsRemove;
    }

    ~ProgressHandler()
    {
        _inventoryModel.ItemChanged -= OnItemChange;
        _levelsStatisticModel.Added -= OnLevelChange;

        _characterUpgrades.Added -= OnUpgradeAdd;
        _characterUpgrades.AllRemoved -= OnUpgradesRemove;

        _characterSkills.Added -= OnSkillAdd;
        _characterSkills.AllRemoved -= OnSkillsRemove;
    }

    private void OnUpgradeAdd(ParametersUpgradeBehaviour upgrade) =>
        _data.AddUpgrade(upgrade.UpgradeType, upgrade.CurrentLevel);

    private void OnUpgradesRemove() =>
        _data.ResetUpgrades();

    private void OnSkillAdd(SkillBehaviour skill) =>
        _data.AddSkill(skill.UpgradeType, skill.CurrentLevel);

    private void OnSkillsRemove() =>
        _data.ResetSkills();

    private void OnLevelChange()
    {
        _data.SetLevels(_levelsStatisticModel.TotalLevel);
    }

    private void OnItemChange(LootType type, int count)
    {
        _data.SetLoot(type, count);
    }

    public void Load()
    {
        if(PlayerPrefs.HasKey(SaveName) == true)
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
        string data = JsonUtility.ToJson(_data);

        PlayerPrefs.SetString(SaveName, data);
        Debug.Log(PlayerPrefs.GetString(SaveName));
    }

    private void InjectData()
    {
        LoadLoot();
        LoadSkills();
        LoadUpgrades();

        _levelsStatisticModel.Set(_data.TotalLevels);
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
