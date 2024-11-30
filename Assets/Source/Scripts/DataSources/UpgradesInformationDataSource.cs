using System;
using System.Collections.Generic;
using Language;
using Skills.Skills;
using Skills.Skills.Pet;
using Skills.Skills.SwordRoundAttack;
using Skills.Skills.ThrowingAxes;

namespace DataSources
{
    public class UpgradesInformationDataSource
    {
        private readonly LanguageProvider _languageProvider;
        private readonly Dictionary<Type, string> _skillsName;
        private readonly Dictionary<Type, string> _skillsDescription;

        public UpgradesInformationDataSource(LanguageProvider languageProvider)
        {
            _languageProvider = languageProvider;

            _skillsName = new ()
            {
                [typeof(SwordRoundAttackSkill)] = _languageProvider.NameSwordRoundAttackSkill,
                [typeof(CritAttackSkill)] = _languageProvider.NameCritAttackSkill,
                [typeof(SplashSkill)] = _languageProvider.NameSplashSkill,
                [typeof(PassiveHealSkill)] = _languageProvider.NamePassiveHealSkill,
                [typeof(AttackSpeedSkill)] = _languageProvider.NameAttackSpeedSkill,
                [typeof(MaxHealthUpSkill)] = _languageProvider.NameMaxHealthUpSkill,
                [typeof(ThrowingAxesSkill)] = _languageProvider.NameThrowingAxesSkill,
                [typeof(PetSkill)] = _languageProvider.NamePetSkill,
            };

            _skillsDescription = new ()
            {
                [typeof(SwordRoundAttackSkill)] = _languageProvider.DescriptionSwordRoundAttackSkill,
                [typeof(CritAttackSkill)] = _languageProvider.DescriptionCritAttackSkill,
                [typeof(SplashSkill)] = _languageProvider.DescriptionSplashSkill,
                [typeof(PassiveHealSkill)] = _languageProvider.DescriptionPassiveHealSkill,
                [typeof(AttackSpeedSkill)] = _languageProvider.DescriptionAttackSpeedSkill,
                [typeof(MaxHealthUpSkill)] = _languageProvider.DescriptionMaxHealthUpSkill,
                [typeof(ThrowingAxesSkill)] = _languageProvider.DescriptionThrowingAxesSkill,
                [typeof(PetSkill)] = _languageProvider.DescriptionPetSkill,
            };
        }

        public string GetName(Type skillType) => _skillsName[skillType];

        public string GetDescription(Type skillType) => _skillsDescription[skillType];
    }
}