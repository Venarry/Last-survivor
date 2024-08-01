using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SkillsSpriteDataSouce
{
    private Dictionary<Type, Sprite> _icons;
    private AssetsProvider _assetProvider;

    public SkillsSpriteDataSouce(AssetsProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public async Task Load()
    {
        Sprite swordRoundSkill = await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconSwordRoundAttack);

        _icons = new()
        {
            { typeof(SwordRoundAttackSkill), swordRoundSkill }
        };
    }

    public Sprite Get(Type skillType)
    {
        return _icons[skillType];
    }
}
