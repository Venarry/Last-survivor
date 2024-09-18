using System.Threading.Tasks;
using UnityEngine;

public class SkillsViewFactory
{
    private readonly SpritesDataSouce _spritesDataSouce;
    private readonly UpgradesInformationDataSource _skillsInformationDataSource;
    private readonly AssetsProvider _assetsProvider;

    public SkillsViewFactory(
        SpritesDataSouce skillsSpriteDataSouce,
        UpgradesInformationDataSource skillsInformationDataSource,
        AssetsProvider assetsProvider)
    {
        _spritesDataSouce = skillsSpriteDataSouce;
        _skillsInformationDataSource = skillsInformationDataSource;
        _assetsProvider = assetsProvider;
    }

    public async Task Load()
    {
        await _assetsProvider.LoadGameObject<SkillToChoose>(AssetsKeys.SkillToChoose);
        await _assetsProvider.LoadGameObject<SkillIcon>(AssetsKeys.SkillIcon);
    }

    public async Task<SkillToChoose> CreateSkillButton(
        Transform parent,
        CharacterUpgradesModel<SkillBehaviour> upgradable,
        SkillsOpener skillsOpener,
        SkillBehaviour skill,
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
        skillToChooseButton.Init(upgradable, skillsOpener, skill);
        skillToChooseButton.SetSkillInformation(icon, skillLevel, maxSkillLevel, name, description, upgradeDescription);

        return skillToChooseButton;
    }

    public async Task<SkillIcon> CreateSkillIcon(System.Type skillType, Transform parent)
    {
        SkillIcon skillIcon = Object.Instantiate(await _assetsProvider.LoadGameObject<SkillIcon>(AssetsKeys.SkillIcon), parent);

        Sprite icon = _spritesDataSouce.Get(skillType);
        skillIcon.Set(icon);

        return skillIcon;
    }
}