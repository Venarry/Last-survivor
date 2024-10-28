using System;

[Serializable]
public class LanguageRu : ILanguageProvider
{
    public string LoadingPart1 => "Загрузка карты";
    public string LoadingPart2 => "Загрузка игрока";
    public string LoadingPart3 => "Загрузка магазина";
    public string LoadingPart4 => "Загрузка целей";
    public string ResetProgressPart1 => "Ты получишь";
    public string ResetProgressPart2 => "монет престижа";
    public string WarningResetProgressPart1 => "Тебе нужен";
    public string WarningResetProgressPart2 => "уровень или выше для сброса прогресса";

    public string TargetNameEnemy => "врагам";
    public string TargetNameWood => "дереву";
    public string TargetNameOre => "руде";
    public string AdditionalDamageHeader => "Дополнительный урон по";
    public string ExperienceIncreaseHeader => "Увеличение получения опыта";
    public string ThrowingAxesCount => "Кол-во топоров";
    public string ThrowingAxesDamage => "Урон топоров от атаки по цели";
    public string RoundSwordCountHeader => "Кол-во мечей";
    public string RoundSwordDamageHeader => "Урон меча";
    public string RoundSwordSizeHeader => "Размер меча";
    public string DayDurationHeader => "Увеличение длительности дня";
    public string AttackCooldown => "Уменьшение время между атаками";
    public string HealthPerSecond => "Здоровье в секунду";
    public string CritDamage => "Критический урон";
    public string CritChance => "Шанс крита";
    public string IncreaseMaxHealth => "Увеличение здоровья";
    public string PetDamageMultiplier => "Коэффициент урона";
    public string PetAttackDelayMultiplier => "Коэффициент скорости атаки";
    public string PetMoveToTargetDelay => "Время пути до цели";
    public string SplashAngle => "Угол сплеша";
    public string SplashDistance => "Дистанция сплеша";
    public string SplashDamage => "Урон от сплеша";

    public string NameSwordRoundAttackSkill => "Летающие мечи";

    public string NameCritAttackSkill => throw new System.NotImplementedException();

    public string NameSplashSkill => throw new System.NotImplementedException();

    public string NamePassiveHealSkill => throw new System.NotImplementedException();

    public string NameAttackSpeedSkill => throw new System.NotImplementedException();

    public string NameMaxHealthUpSkill => throw new System.NotImplementedException();

    public string NameThrowingAxesSkill => throw new System.NotImplementedException();
}
