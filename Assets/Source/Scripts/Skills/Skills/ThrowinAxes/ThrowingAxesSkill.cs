using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class ThrowingAxesSkill : SkillBehaviour
{
    private const float SpawnDelay = 0.2f;

    private readonly WaitForSeconds _spawnDelay = new(SpawnDelay);
    private readonly TargetsProvider _targetsProvider;
    private readonly ThrowingAxesFactory _throwingAxesFactory;
    private readonly Transform _owner;
    private readonly CoroutineProvider _coroutineProvider;
    private float _damageMultiplier = 0.4f;
    private float _throwDistance = 10f;
    private float _axesCounter = 0;

    private Vector3 SpawnPosition => _owner.position + new Vector3(0, 1f, 0);

    public ThrowingAxesSkill(
        TargetsProvider targetsProvider,
        ThrowingAxesFactory throwingAxesFactory,
        Transform owner,
        CoroutineProvider coroutineProvider)
    {
        _targetsProvider = targetsProvider;
        _throwingAxesFactory = throwingAxesFactory;
        _owner = owner;
        _coroutineProvider = coroutineProvider;
    }

    public override SkillTickType SkillTickType => SkillTickType.EveryTick;
    public override bool HasCooldown => false;

    public override void Apply()
    {
        if (_axesCounter > 0)
            return;

        Ray ray = new(SpawnPosition, Vector3.forward);

        if (_targetsProvider.TryGetRayTargets(ray, _throwDistance, out _))
        {
            _coroutineProvider.StartCoroutine(SpawnAxes(CurrentLevel));
        }
    }

    public override void Disable()
    {
    }

    public override string GetUpLevelDescription()
    {
        return "";
    }

    private IEnumerator SpawnAxes(float count)
    {
        for (int i = 0; i < count; i++)
        {
            _axesCounter++;
            Task<ThrowingAxe> axeTask = _throwingAxesFactory.Create(SpawnPosition, _throwDistance, _owner, _damageMultiplier);

            yield return axeTask;

            ThrowingAxe throwingAxe = axeTask.Result;
            throwingAxe.Coming += OnAxeCome;

            yield return _spawnDelay;
        }
    }

    private void OnAxeCome(ThrowingAxe axe)
    {
        axe.Coming -= OnAxeCome;
        _axesCounter--;
    }
}
