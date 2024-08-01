using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SkillsSpriteDataSouce
{
    private Dictionary<Type, Sprite> _icons;

    public async Task Load()
    {
        Sprite swordRoundSkill = await Addressables.LoadAssetAsync<Sprite>(ResourcesPath.SkillIconSwordRoundAttack).Task;

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
