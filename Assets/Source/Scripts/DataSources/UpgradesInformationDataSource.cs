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
            [GameParameters.CodeRu] = "Летающие мечи",
            [GameParameters.CodeEn] = "Flying swords",
            [GameParameters.CodeTr] = "Uçan Kılıçlar",
        },

        [typeof(CritAttackSkill)] = new()
        {
            [GameParameters.CodeRu] = "Критическая атака",
            [GameParameters.CodeEn] = "Critical attack",
            [GameParameters.CodeTr] = "Kritik saldırı",
        },

        [typeof(SplashSkill)] = new()
        {
            [GameParameters.CodeRu] = "Сплеш",
            [GameParameters.CodeEn] = "Splash",
            [GameParameters.CodeTr] = "Sıçramak",
        },

        [typeof(PassiveHealSkill)] = new()
        {
            [GameParameters.CodeRu] = "Пассивное исцеление",
            [GameParameters.CodeEn] = "Passive heal",
            [GameParameters.CodeTr] = "Pasif iyileşme",
        },

        [typeof(AttackSpeedSkill)] = new()
        {
            [GameParameters.CodeRu] = "Перезарядка атаки",
            [GameParameters.CodeEn] = "Attack cooldown",
            [GameParameters.CodeTr] = "Saldırı bekleme süresi",
        },

        [typeof(MaxHealthUpSkill)] = new()
        {
            [GameParameters.CodeRu] = "Максимальное здоровье",
            [GameParameters.CodeEn] = "Max health",
            [GameParameters.CodeTr] = "Maksimum sağlık",
        },

        [typeof(ThrowingAxesSkill)] = new()
        {
            [GameParameters.CodeRu] = "Метательные топоры",
            [GameParameters.CodeEn] = "Throwing axes",
            [GameParameters.CodeTr] = "Balta atma",
        },

        [typeof(PetSkill)] = new()
        {
            [GameParameters.CodeRu] = "Питомец",
            [GameParameters.CodeEn] = "Pet",
            [GameParameters.CodeTr] = "Evcil hayvan",
        },
    };

    private readonly Dictionary<Type, Dictionary<string, string>> _skillsDescription = new()
    {
        [typeof(SwordRoundAttackSkill)] = new()
        {
            [GameParameters.CodeRu] = "Вокруг персонажа периодически появляются вращающиеся мечи",
            [GameParameters.CodeEn] = "Spinning swords periodically appear around the character",
            [GameParameters.CodeTr] = "Dönen kılıçlar periyodik olarak karakterin etrafında belirir",
        },

        [typeof(CritAttackSkill)] = new()
        {
            [GameParameters.CodeRu] = "Каждая атака имеет шанс нанести увеличенный урон",
            [GameParameters.CodeEn] = "Each attack has a chance to deal increased damage",
            [GameParameters.CodeTr] = "Her saldırının daha fazla hasar verme şansı vardır",
        },

        [typeof(SplashSkill)] = new()
        {
            [GameParameters.CodeRu] = "При атаке персонаж наносит урон соседним целям",
            [GameParameters.CodeEn] = "When attacking, the character deals damage to neighboring targets",
            [GameParameters.CodeTr] = "Saldırırken, karakter komşu hedeflere zarar verir",
        },

        [typeof(PassiveHealSkill)] = new()
        {
            [GameParameters.CodeRu] = "Персонаж пассивно исцеляется",
            [GameParameters.CodeEn] = "The character is passively healing",
            [GameParameters.CodeTr] = "Karakter pasif olarak iyileşiyor",
        },

        [typeof(AttackSpeedSkill)] = new()
        {
            [GameParameters.CodeRu] = "Уменьшает перезарядку между атаками",
            [GameParameters.CodeEn] = "Reduce attack cooldown",
            [GameParameters.CodeTr] = "Saldırılar arasındaki aşırı şarjı azaltır",
        },

        [typeof(MaxHealthUpSkill)] = new()
        {
            [GameParameters.CodeRu] = "Увеличивает максимальное здоровье",
            [GameParameters.CodeEn] = "Increase your max health",
            [GameParameters.CodeTr] = "Maksimum sağlığınızı artırın",
        },

        [typeof(ThrowingAxesSkill)] = new()
        {
            [GameParameters.CodeRu] = "Кидает топоры вперед",
            [GameParameters.CodeEn] = "Throw axes in forward",
            [GameParameters.CodeTr] = "Eksenleri öne doğru atın",
        },

        [typeof(PetSkill)] = new()
        {
            [GameParameters.CodeRu] = "Бегает вокруг персонажа и атакует цели с параметрами атаки игрока",
            [GameParameters.CodeEn] = "Runs around the character and attacks targets with the player's attack parameters",
            [GameParameters.CodeTr] = "Karakterin etrafında koşar ve oyuncunun saldırı parametreleriyle hedeflere saldırır",
        },
    };

    public string GetName(Type skillType) => GetData(_skillsName, skillType);

    public string GetDescription(Type skillType) => GetData(_skillsDescription, skillType);

    private string GetData(Dictionary<Type, Dictionary<string, string>> source, Type skillType)
    {
        if (source[skillType].ContainsKey(YandexGame.lang) == true)
        {
            return source[skillType][YandexGame.lang];
        }
        else
        {
            return source[skillType][GameParameters.CodeEn];
        }
    }
}
