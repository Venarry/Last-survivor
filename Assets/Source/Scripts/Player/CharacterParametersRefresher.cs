public class CharacterParametersRefresher
{
    private readonly LevelsStatisticModel _levelsStatisticModel;
    private readonly ExperienceModel _experienceModel;
    private readonly CharacterSkillsModel _characterSkillsModel;

    public CharacterParametersRefresher(LevelsStatisticModel levelsStatisticModel, ExperienceModel experienceModel, CharacterSkillsModel characterSkillsModel)
    {
        _levelsStatisticModel = levelsStatisticModel;
        _experienceModel = experienceModel;
        _characterSkillsModel = characterSkillsModel;
    }

    public void Enable()
    {
        _levelsStatisticModel.Added += OnWaveAdd;
    }

    public void Disable()
    {
        _levelsStatisticModel.Added -= OnWaveAdd;
    }

    private void OnWaveAdd()
    {
        if (_levelsStatisticModel.CurrentWave != 0)
            return;

        _characterSkillsModel.RemoveAll();
        _experienceModel.Reset();
    }
}
