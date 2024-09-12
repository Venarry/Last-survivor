using System.Collections;
using UnityEngine;

public class CharacterParametersRefresher
{
    private readonly LevelsStatisticModel _levelsStatisticModel;
    private readonly ExperienceModel _experienceModel;
    private readonly HealthModel _healthModel;
    private readonly CharacterUpgradesModel<SkillBehaviour> _characterSkillsModel;
    private readonly WaitForSeconds _waitForExperienceDisable = new(seconds: 2);
    private readonly CoroutineProvider _coroutineProvider;

    public CharacterParametersRefresher(
        LevelsStatisticModel levelsStatisticModel,
        ExperienceModel experienceModel,
        HealthModel healthModel,
        CharacterUpgradesModel<SkillBehaviour> characterSkillsModel,
        CoroutineProvider coroutineProvider)
    {
        _levelsStatisticModel = levelsStatisticModel;
        _experienceModel = experienceModel;
        _healthModel = healthModel;
        _characterSkillsModel = characterSkillsModel;
        _coroutineProvider = coroutineProvider;
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
        if (_levelsStatisticModel.CurrentLevel != 0)
            return;

        _characterSkillsModel.RemoveAll();
        _experienceModel.Reset();
        _healthModel.Restore();

        _coroutineProvider.StartCoroutine(DisableExperienceBehaviour());
    }

    private IEnumerator DisableExperienceBehaviour()
    {
        _experienceModel.DisableBehaviour();
        yield return _waitForExperienceDisable;
        _experienceModel.EnableBeahviour();
    }
}
