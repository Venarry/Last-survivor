﻿using System.Threading.Tasks;
using UnityEngine;

public class SkillsViewFactory
{
    private readonly SpritesDataSouce _spritesDataSouce;
    private readonly SkillsInformationDataSource _skillsInformationDataSource;
    private readonly AssetsProvider _assetsProvider;

    public SkillsViewFactory(
        SpritesDataSouce skillsSpriteDataSouce,
        SkillsInformationDataSource skillsInformationDataSource,
        AssetsProvider assetsProvider)
    {
        _spritesDataSouce = skillsSpriteDataSouce;
        _skillsInformationDataSource = skillsInformationDataSource;
        _assetsProvider = assetsProvider;
    }

    public async Task<SkillToChoose> CreateSkillButton(
        Transform parent,
        IUpgradable<ISkill> upgradable,
        SkillsOpener skillsOpener,
        ISkill skill,
        int skillLevel,
        int maxSkillLevel,
        string upgradeDescription)
    {
        SkillToChoose skillToChoosePrefab = await _assetsProvider.LoadGameObject<SkillToChoose>(AssetsKeys.SkillToChoose);

        System.Type skillType = skill.GetType();
        Sprite icon = _spritesDataSouce.Get(skillType);
        string name = _skillsInformationDataSource.GetName(skillType);
        string description = _skillsInformationDataSource.GetDescription(skillType);

        SkillToChoose skillToChooseButton = Object.Instantiate(skillToChoosePrefab, parent);
        skillToChooseButton.Init(upgradable, skillsOpener, icon, skill);
        skillToChooseButton.SetSkillInformation(skillLevel, maxSkillLevel, name, description, upgradeDescription);

        return skillToChooseButton;
    }

    public async void CreateSkillIcon(System.Type skillType, Transform parent)
    {
        SkillIcon skillIcon = Object.Instantiate(await _assetsProvider.LoadGameObject<SkillIcon>(AssetsKeys.SkillIcon), parent);

        Sprite icon = _spritesDataSouce.Get(skillType);
        skillIcon.Set(icon);
    }
}