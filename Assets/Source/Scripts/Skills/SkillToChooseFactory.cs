using System.Threading.Tasks;
using UnityEngine;

public class SkillToChooseFactory
{
    private SpritesDataSouce _skillsSpriteDataSouce;
    private SkillsInformationDataSource _skillsInformationDataSource;
    private AssetsProvider _assetsProvider;

    public SkillToChooseFactory(
        SpritesDataSouce skillsSpriteDataSouce,
        SkillsInformationDataSource skillsInformationDataSource,
        AssetsProvider assetsProvider)
    {
        _skillsSpriteDataSouce = skillsSpriteDataSouce;
        _skillsInformationDataSource = skillsInformationDataSource;
        _assetsProvider = assetsProvider;
    }

    public async Task<SkillToChoose> Create(
        Transform parent,
        IUpgradable<ISkill> upgradable,
        SkillsOpener skillsOpener,
        ISkill skill,
        int skillLevel,
        int maxSkillLevel)
    {
        SkillToChoose skillToChoosePrefab = await _assetsProvider.LoadGameObject<SkillToChoose>(AssetsKeys.SkillToChoose);
        Sprite icon = _skillsSpriteDataSouce.Get(skill.GetType());
        string name = _skillsInformationDataSource.GetName(skill.GetType());
        string description = _skillsInformationDataSource.GetDescription(skill.GetType());

        SkillToChoose skillToChooseButton = Object.Instantiate(skillToChoosePrefab, parent);
        skillToChooseButton.Init(upgradable, skillsOpener, icon, skill);
        skillToChooseButton.SetSkillInformation(skillLevel, maxSkillLevel, name, description);

        return skillToChooseButton;
    }
}
