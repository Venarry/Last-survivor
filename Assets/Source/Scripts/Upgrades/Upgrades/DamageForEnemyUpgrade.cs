﻿public class DamageForEnemyUpgrade : DamageUpgrade
{
    private readonly DamageForEnemyBuff _buff = new();

    public DamageForEnemyUpgrade(CharacterBuffsModel characterBuffsModel) : base(characterBuffsModel)
    {
    }

    public override UpgradeType UpgradeType => UpgradeType.DamageForEnemy;
    protected override DamageBuff DamageBuff => _buff;
    protected override string TargetNameRu => "врагам";
    protected override string TargetNameEn => "enemy";
    protected override string TargetNameTr => "düşmanlara";
}