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
}
