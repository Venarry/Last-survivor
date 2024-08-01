using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SkillsSpriteDataSouce
{
    private Dictionary<Type, Sprite> _icons;
    private AssetProvider _assetProvider;

    public SkillsSpriteDataSouce(AssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public async Task Load()
    {
        Sprite swordRoundSkill = await _assetProvider.Load<Sprite>(ResourcesPath.SkillIconSwordRoundAttack);

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
