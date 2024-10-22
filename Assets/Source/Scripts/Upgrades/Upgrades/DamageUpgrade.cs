using System;
using YG;

public abstract class DamageUpgrade : ParametersUpgradeBehaviour
{
    protected DamageUpgrade(CharacterBuffsModel characterBuffsModel) : base(characterBuffsModel)
    {
    }

    protected abstract DamageBuff DamageBuff { get; }
    protected virtual float DamagePerLevel { get; } = 0.1f;
    protected abstract string TargetNameRu { get; }
    protected abstract string TargetNameEn { get; }
    protected abstract string TargetNameTr { get; }
    private float Damage => DamagePerLevel * CurrentLevel;

    public override void Apply()
    {
        CharacterBuffsModel.Add(DamageBuff);
        DamageBuff.SetParameters(Damage);
    }

    protected override void OnLevelChange()
    {
        DamageBuff.SetParameters(Damage);
    }

    public override void Disable()
    {
        CharacterBuffsModel.Remove(DamageBuff);
    }

    public override string GetUpLevelDescription()
    {
        string additionalDamageHeader;
        string targetNameHeader;

        switch (YandexGame.lang)
        {
            case GameParameters.CodeRu:
                additionalDamageHeader = "Дополнительный урон по";
                targetNameHeader = TargetNameRu;
                break;

            case GameParameters.CodeTr:
                additionalDamageHeader = "Ek hasar";
                targetNameHeader = TargetNameTr;
                break;

            default:
                additionalDamageHeader = "Additional damage by";
                targetNameHeader = TargetNameEn;
                break;
        }

        string description = $"{additionalDamageHeader} {targetNameHeader}:\n{CurrentLevel * DamagePerLevel} + {Decorate(DamagePerLevel.ToString())}";

        return description;
    }
}
