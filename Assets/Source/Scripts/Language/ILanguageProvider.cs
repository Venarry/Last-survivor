public interface ILanguageProvider
{
    public string LoadingPart1 { get; }
    public string LoadingPart2 { get; }
    public string LoadingPart3 { get;}
    public string LoadingPart4 { get;}
    public string ResetProgressPart1 { get; }
    public string ResetProgressPart2 { get; }
    public string WarningResetProgressPart1 { get; }
    public string WarningResetProgressPart2 { get; }


    public string TargetNameEnemy { get; }
    public string TargetNameWood { get; }
    public string TargetNameOre { get; }
    public string AdditionalDamageHeader { get; }
    public string ExperienceIncreaseHeader { get; }
    public string ThrowingAxesCount { get; }
    public string ThrowingAxesDamage { get; }
    public string RoundSwordCountHeader { get; }
    public string RoundSwordDamageHeader { get; }
    public string RoundSwordSizeHeader { get; }
    public string DayDurationHeader { get; }
    public string AttackCooldown { get; }
    public string HealthPerSecond { get; }
    public string CritDamage { get; }
    public string CritChance { get; }
    public string IncreaseMaxHealth { get; }
    public string PetDamageMultiplier { get; }
    public string PetAttackDelayMultiplier { get; }
    public string PetMoveToTargetDelay { get; }
    public string SplashAngle { get; }
    public string SplashDistance { get; }
    public string SplashDamage { get; }
    public string NameSwordRoundAttackSkill { get; }
    public string NameCritAttackSkill { get; }
    public string NameSplashSkill { get; }
    public string NamePassiveHealSkill { get; }
    public string NameAttackSpeedSkill { get; }
    public string NameMaxHealthUpSkill { get; }
    public string NameThrowingAxesSkill { get; }
}
