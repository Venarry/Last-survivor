using System;

[Serializable]
public class LanguageProvider
{
    public string LoadingPart1 = "Загрузка карты";
    public string LoadingPart2 = "Загрузка игрока";
    public string LoadingPart3 = "Загрузка магазина";
    public string LoadingPart4 = "Загрузка целей";
    public string ResetProgressPart1 = "Ты получишь";
    public string ResetProgressPart2 = "монет престижа";
    public string WarningResetProgressPart1 = "Тебе нужен";
    public string WarningResetProgressPart2 = "уровень или выше для сброса прогресса";

    public string TargetNameEnemy = "врагам";
    public string TargetNameWood = "дереву";
    public string TargetNameOre = "руде";
    public string AdditionalDamageHeader = "Дополнительный урон по";
    public string ExperienceIncreaseHeader = "Увеличение получения опыта";
    public string ThrowingAxesCount = "Кол-во топоров";
    public string ThrowingAxesDamage = "Урон топоров от атаки по цели";
    public string RoundSwordCountHeader = "Кол-во мечей";
    public string RoundSwordDamageHeader = "Урон меча";
    public string RoundSwordSizeHeader = "Размер меча";
    public string DayDurationHeader = "Увеличение длительности дня";
    public string AttackCooldown = "Уменьшение время между атаками";
    public string HealthPerSecond = "Здоровье в секунду";
    public string CritDamage = "Критический урон";
    public string CritChance = "Шанс крита";
    public string IncreaseMaxHealth = "Увеличение здоровья";
    public string PetDamageMultiplier = "Коэффициент урона";
    public string PetAttackDelayMultiplier = "Коэффициент скорости атаки";
    public string PetMoveToTargetDelay = "Время пути до цели";
    public string SplashAngle = "Угол сплеша";
    public string SplashDistance = "Дистанция сплеша";
    public string SplashDamage = "Урон от сплеша";

    public string NameSwordRoundAttackSkill = "Летающие мечи";
    public string NameCritAttackSkill = "Критическая атака";
    public string NameSplashSkill = "Сплеш";
    public string NamePassiveHealSkill = "Пассивное исцеление";
    public string NameAttackSpeedSkill = "Перезарядка атаки";
    public string NameMaxHealthUpSkill = "Максимальное здоровье";
    public string NameThrowingAxesSkill = "Метательные топоры";
    public string NamePetSkill = "Питомец";

    public string DescriptionSwordRoundAttackSkill = "Вокруг персонажа периодически появляются вращающиеся мечи";
    public string DescriptionCritAttackSkill = "Каждая атака имеет шанс нанести увеличенный урон";
    public string DescriptionSplashSkill = "При атаке персонаж наносит урон соседним целям";
    public string DescriptionPassiveHealSkill = "Персонаж пассивно исцеляется";
    public string DescriptionAttackSpeedSkill = "Уменьшает перезарядку между атаками";
    public string DescriptionMaxHealthUpSkill = "Увеличивает максимальное здоровье";
    public string DescriptionThrowingAxesSkill = "Кидает топоры вперед";
    public string DescriptionPetSkill = "Бегает вокруг игрока и атакует цель с параметрами атаки игрока";
}
