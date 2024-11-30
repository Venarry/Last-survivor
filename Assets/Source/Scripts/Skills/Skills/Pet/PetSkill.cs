using System.Collections.Generic;
using Language;
using UnityEngine;

namespace Skills.Skills.Pet
{
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

        public PetSkill(PetFactory petFactory, Transform owner, LanguageProvider languageProvider)
            : base(languageProvider)
        {
            _petFactory = petFactory;
            _owner = owner;
        }

        public override UpgradeType UpgradeType => UpgradeType.Pet;
        public override SkillTickType SkillTickType => SkillTickType.AwakeTick;
        public override bool HasCooldown => false;
        private float DamageMultiplier => _baseDamageMultiplier + (_damageMultiplierPerLevel * Mathf.Max(CurrentLevel - 1, 0));
        private float AttackCooldownMultiplier => _baseAttackCooldownMultiplier + (_attackCooldownMultiplierPerLevel * Mathf.Max(CurrentLevel - 1, 0));
        private float MoveToTargetDelay => _baseMoveToTargetDelay + (_moveToTargetDelayPerLevel * Mathf.Max(CurrentLevel - 1, 0));

        public override async void Apply()
        {
            _pet = await _petFactory.Create(_owner.transform.position);
            _pet.SetParameters(DamageMultiplier, AttackCooldownMultiplier, MoveToTargetDelay);
        }

        public override void Disable()
        {
            UnityEngine.Object.Destroy(_pet.gameObject);
        }

        public override string GetUpLevelDescription()
        {
            List<float> damage = new ();
            List<float> cooldown = new ();
            List<float> moveDelay = new ();

            for (int i = 0; i < MaxLevel; i++)
            {
                damage.Add(_baseDamageMultiplier + (_damageMultiplierPerLevel * i));
                cooldown.Add(_baseAttackCooldownMultiplier + (_attackCooldownMultiplierPerLevel * i));
                moveDelay.Add(_baseMoveToTargetDelay + (_moveToTargetDelayPerLevel * i));
            }

            return $"{LanguageProvider.PetDamageMultiplier}\n{GetAllLevelsUpgradesText(damage.ToArray())}\n" +
                $"{LanguageProvider.PetAttackDelayMultiplier}\n{GetAllLevelsUpgradesText(cooldown.ToArray())}\n" +
                $"{LanguageProvider.PetMoveToTargetDelay}\n{GetAllLevelsUpgradesText(moveDelay.ToArray())}";
        }

        protected override void OnLevelChange()
        {
            if (_pet == null)
            {
                return;
            }

            _pet.SetParameters(DamageMultiplier, AttackCooldownMultiplier, MoveToTargetDelay);
        }
    }
}