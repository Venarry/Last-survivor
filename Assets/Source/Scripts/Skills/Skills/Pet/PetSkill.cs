using System.Collections.Generic;
using UnityEngine;

public class PetSkill : SkillBehaviour
{
    private readonly float _baseDamageMultiplier = 0.6f;
    private readonly float _baseAttackCooldownMultiplier = 1.8f;
    private readonly float _baseMoveToTargetDelay = 1.6f;

    private readonly float _damageMultiplierPerLevel = 0.1f;
    private readonly float _attackCooldownMultiplierPerLevel = -0.2f;
    private readonly float _moveToTargetDelayPerLevel = -0.4f;
    
    private readonly PetFactory _petFactory;
    private readonly Transform _owner;
    private Pet _pet;

    public PetSkill(PetFactory petFactory, Transform owner)
    {
        _petFactory = petFactory;
        _owner = owner;
    }

    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override async void Apply()
    {
        _pet = await _petFactory.Create(_owner.transform.position);
        _pet.SetParameters(_baseDamageMultiplier, _baseAttackCooldownMultiplier, _baseMoveToTargetDelay);
    }

    protected override void OnLevelAdd()
    {
        int levelMultiplier = CurrentLevel - 1;

        float damageMultiplier = _baseDamageMultiplier + _damageMultiplierPerLevel * levelMultiplier;
        float attackCooldownMultiplier = _baseAttackCooldownMultiplier + _attackCooldownMultiplierPerLevel * levelMultiplier;
        float moveToTargetDelay = _baseMoveToTargetDelay + _moveToTargetDelayPerLevel * levelMultiplier;

        _pet.SetParameters(damageMultiplier, attackCooldownMultiplier, moveToTargetDelay);
    }

    public override void Disable()
    {
        Object.Destroy(_pet.gameObject);
    }

    public override string GetUpLevelDescription()
    {
        List<float> damage = new();
        List<float> cooldown = new();
        List<float> moveDelay = new();

        for (int i = 0; i < MaxLevel; i++)
        {
            damage.Add(_baseDamageMultiplier + _damageMultiplierPerLevel * i);
            cooldown.Add(_baseAttackCooldownMultiplier + _attackCooldownMultiplierPerLevel * i);
            moveDelay.Add(_baseMoveToTargetDelay + _moveToTargetDelayPerLevel * i);
        }

        return $"Damage multiplier\n{ GetAllLevelsUpgradesText(damage.ToArray()) }\n" +
            $"Attack cooldown multiplier\n{ GetAllLevelsUpgradesText(cooldown.ToArray()) }\n" +
            $"Move to target delay\n{ GetAllLevelsUpgradesText(moveDelay.ToArray()) }";
    }
}
