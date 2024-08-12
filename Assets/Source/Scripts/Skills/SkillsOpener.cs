using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class SkillsOpener : MonoBehaviour
{
    private const int SkillsInChoose = 3;

    [SerializeField] private GameObject _skillsParent;
    [SerializeField] private SkillToChoose _skillsPrefab;

    private List<SkillToChoose> _spawnedSkill = new();
    private SkillToChooseFactory _skillToChooseFactory;
    private CharacterSkills _characterSkills;
    private ExperienceModel _experienceModel;
    private SkillsFactory _skillsFactory;

    public void Init(
        SkillToChooseFactory skillToChooseFactory,
        CharacterSkills characterSkills,
        ExperienceModel experienceModel,
        SkillsFactory skillsFactory)
    {
        _skillToChooseFactory = skillToChooseFactory;
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

    private async void SpawnSkill(ISkill skill, int level, int maxLevel)
    {
        SkillToChoose skillButton = await _skillToChooseFactory
            .Create(_skillsParent.transform, _characterSkills, this, skill, level, maxLevel);

        _spawnedSkill.Add(skillButton);
    }
}

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
