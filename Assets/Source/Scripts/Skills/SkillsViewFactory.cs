using System.Threading.Tasks;
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
        int maxSkillLevel)
    {
        SkillToChoose skillToChoosePrefab = await _assetsProvider.LoadGameObject<SkillToChoose>(AssetsKeys.SkillToChoose);
        Sprite icon = _spritesDataSouce.Get(skill.GetType());
        string name = _skillsInformationDataSource.GetName(skill.GetType());
        string description = _skillsInformationDataSource.GetDescription(skill.GetType());

        SkillToChoose skillToChooseButton = Object.Instantiate(skillToChoosePrefab, parent);
        skillToChooseButton.Init(upgradable, skillsOpener, icon, skill);
        skillToChooseButton.SetSkillInformation(skillLevel, maxSkillLevel, name, description);

        return skillToChooseButton;
    }

    public async void CreateSkillIcon(System.Type skillType, Transform parent)
    {
        SkillIcon skillIcon = Object.Instantiate(await _assetsProvider.LoadGameObject<SkillIcon>(AssetsKeys.SkillIcon), parent);

        Sprite icon = _spritesDataSouce.Get(skillType);
        skillIcon.Set(icon);
    }
}