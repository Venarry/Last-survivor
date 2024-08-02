using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SkillsSpriteDataSouce
{
    private readonly AssetsProvider _assetProvider;
    private Dictionary<Type, Sprite> _icons;

    public SkillsSpriteDataSouce(AssetsProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public async Task Load()
    {
        _icons = new()
        {
            { typeof(SwordRoundAttackSkill), await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconSwordRoundAttack) },
            { typeof(CritAttackSkill), await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconCritAttack) },
            { typeof(SplashSkill), await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconSplash) },
            { typeof(PassiveHealSkill), await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconPassiveHeal) },
        };
    }

    public Sprite Get(Type skillType)
    {
        return _icons[skillType];
    }
}
