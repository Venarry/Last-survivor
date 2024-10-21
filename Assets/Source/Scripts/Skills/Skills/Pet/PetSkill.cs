using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class PetSkill : SkillBehaviour
{
    private readonly float _baseDamageMultiplier = 0.6f;
    private readonly float _baseAttackCooldownMultiplier = 1.8f;
    private readonly float _baseMoveToTargetDelay = 2f;

    private readonly float _damageMultiplierPerLevel = 0.1f;
    private readonly float _attackCooldownMultiplierPerLevel = -0.2f;
    private readonly float _moveToTargetDelayPerLevel = -0.4f;
    
    private readonly PetFactory _petFactory;
    private readonly Transform _owner;
    private Pet _pet;

    private float DamageMultiplier => _baseDamageMultiplier + _damageMultiplierPerLevel * Mathf.Max(CurrentLevel - 1, 0);
    private float AttackCooldownMultiplier => _baseAttackCooldownMultiplier + _attackCooldownMultiplierPerLevel * Mathf.Max(CurrentLevel - 1, 0);
    private float MoveToTargetDelay => _baseMoveToTargetDelay + _moveToTargetDelayPerLevel * Mathf.Max(CurrentLevel - 1, 0);

    public PetSkill(PetFactory petFactory, Transform owner)
    {
        _petFactory = petFactory;
        _owner = owner;
    }

    public override UpgradeType UpgradeType => UpgradeType.Pet;
    public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
    public override bool HasCooldown => false;

    public override async void Apply()
    {
        _pet = await _petFactory.Create(_owner.transform.position);
        _pet.SetParameters(DamageMultiplier, AttackCooldownMultiplier, MoveToTargetDelay);
    }

    protected override void OnLevelChange()
    {
        if (_pet == null)
            return;

        _pet.SetParameters(DamageMultiplier, AttackCooldownMultiplier, MoveToTargetDelay);
    }

    public override void Disable()
    {
        UnityEngine.Object.Destroy(_pet.gameObject);
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

        string damageMultiplierText;
        string attackCooldownText;
        string moveToTargetDelayText;

        switch (YandexGame.lang)
        {
            case GameParameters.CodeRu:
                damageMultiplierText = "Коэффициент урона";
                attackCooldownText = "Коэффициент скорости атаки";
                moveToTargetDelayText = "Время пути до цели";
                break;

            case GameParameters.CodeEn:
                damageMultiplierText = "Damage multiplier";
                attackCooldownText = "Attack cooldown multiplier";
                moveToTargetDelayText = "Move to target delay";
                break;

            case GameParameters.CodeTr:
                damageMultiplierText = "Hasar çarpanı";
                attackCooldownText = "Saldırı bekleme süresi çarpanı";
                moveToTargetDelayText = "Hedef gecikmeye git";
                break;

            default:
                throw new ArgumentNullException(nameof(YandexGame.lang));
        }

        return $"{damageMultiplierText}\n{ GetAllLevelsUpgradesText(damage.ToArray()) }\n" +
            $"{attackCooldownText}\n{ GetAllLevelsUpgradesText(cooldown.ToArray()) }\n" +
            $"{moveToTargetDelayText}\n{ GetAllLevelsUpgradesText(moveDelay.ToArray()) }";
    }
}
