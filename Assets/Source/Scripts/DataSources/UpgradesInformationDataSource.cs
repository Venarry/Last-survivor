using System;
using System.Collections.Generic;
using YG;

public class UpgradesInformationDataSource
{
    private const string CodeRu = "ru";
    private const string CodeEn = "en";
    private const string CodeTr = "tr";

    private readonly Dictionary<Type, Dictionary<string, string>> _skillsName = new()
    {
        [typeof(SwordRoundAttackSkill)] = new()
        {
            [CodeRu] = "Летающие мечи",
            [CodeEn] = "Flying swords",
            [CodeTr] = "Uçan Kılıçlar",
        },

        [typeof(CritAttackSkill)] = new()
        {
            [CodeRu] = "Критическая атака",
            [CodeEn] = "Critical attack",
            [CodeTr] = "Kritik saldırı",
        },

        [typeof(SplashSkill)] = new()
        {
            [CodeRu] = "Сплеш",
            [CodeEn] = "Splash",
            [CodeTr] = "Sıçramak",
        },

        [typeof(PassiveHealSkill)] = new()
        {
            [CodeRu] = "Пассивное исцеление",
            [CodeEn] = "Passive heal",
            [CodeTr] = "Pasif iyileşme",
        },

        [typeof(AttackSpeedSkill)] = new()
        {
            [CodeRu] = "Перезарядка атаки",
            [CodeEn] = "Attack cooldown",
            [CodeTr] = "Saldırı bekleme süresi",
        },

        [typeof(MaxHealthUpSkill)] = new()
        {
            [CodeRu] = "Максимальное здоровье",
            [CodeEn] = "Max health",
            [CodeTr] = "Maksimum sağlık",
        },

        [typeof(ThrowingAxesSkill)] = new()
        {
            [CodeRu] = "Метательные топоры",
            [CodeEn] = "Throwing axes",
            [CodeTr] = "Balta atma",
        },

        [typeof(PetSkill)] = new()
        {
            [CodeRu] = "Питомец",
            [CodeEn] = "Pet",
            [CodeTr] = "Evcil hayvan",
        },
    };

    private readonly Dictionary<Type, Dictionary<string, string>> _skillsDescription = new()
    {
        [typeof(SwordRoundAttackSkill)] = new()
        {
            [CodeRu] = "Вокруг персонажа периодически появляются вращающиеся мечи",
            [CodeEn] = "Spinning swords periodically appear around the character",
            [CodeTr] = "Dönen kılıçlar periyodik olarak karakterin etrafında belirir",
        },

        [typeof(CritAttackSkill)] = new()
        {
            [CodeRu] = "Каждая атака имеет шанс нанести увеличенный урон",
            [CodeEn] = "Each attack has a chance to deal increased damage",
            [CodeTr] = "Her saldırının daha fazla hasar verme şansı vardır",
        },

        [typeof(SplashSkill)] = new()
        {
            [CodeRu] = "При атаке персонаж наносит урон соседним целям",
            [CodeEn] = "When attacking, the character deals damage to neighboring targets",
            [CodeTr] = "Saldırırken, karakter komşu hedeflere zarar verir",
        },

        [typeof(PassiveHealSkill)] = new()
        {
            [CodeRu] = "Персонаж пассивно исцеляется",
            [CodeEn] = "The character is passively healing",
            [CodeTr] = "Karakter pasif olarak iyileşiyor",
        },

        [typeof(AttackSpeedSkill)] = new()
        {
            [CodeRu] = "Уменьшает перезарядку между атаками",
            [CodeEn] = "Reduce attack cooldown",
            [CodeTr] = "Saldırılar arasındaki aşırı şarjı azaltır",
        },

        [typeof(MaxHealthUpSkill)] = new()
        {
            [CodeRu] = "Увеличивает максимальное здоровье",
            [CodeEn] = "Increase your max health",
            [CodeTr] = "Maksimum sağlığınızı artırın",
        },

        [typeof(ThrowingAxesSkill)] = new()
        {
            [CodeRu] = "Кидает топоры вперед",
            [CodeEn] = "Throw axes in forward",
            [CodeTr] = "Eksenleri öne doğru atın",
        },

        [typeof(PetSkill)] = new()
        {
            [CodeRu] = "Бегает вокруг персонажа и атакует цели с параметрами атаки игрока",
            [CodeEn] = "Runs around the character and attacks targets with the player's attack parameters",
            [CodeTr] = "Karakterin etrafında koşar ve oyuncunun saldırı parametreleriyle hedeflere saldırır",
        },
    };

    public string GetName(Type skillType) => _skillsName[skillType][YandexGame.lang];
    public string GetDescription(Type skillType) => _skillsDescription[skillType][YandexGame.lang];
}
