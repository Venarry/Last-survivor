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
        Sprite swordRoundSkill = await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconSwordRoundAttack);
        Sprite critAttackSkill = await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconCritAttack);

        _icons = new()
        {
            { typeof(SwordRoundAttackSkill), swordRoundSkill },
            { typeof(CritAttackSkill), critAttackSkill },
        };
    }

    public Sprite Get(Type skillType)
    {
        return _icons[skillType];
    }
}
