using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpritesDataSouce
{
    private readonly AssetsProvider _assetProvider;
    private Dictionary<Type, Sprite> _skillsIcons;
    private Dictionary<LootType, Sprite> _lootIcons;

    public SpritesDataSouce(AssetsProvider assetProvider)
    {
        _assetProvider = assetProvider;
    }

    public async Task Load()
    {
        _skillsIcons = new()
        {
            [typeof(SwordRoundAttackSkill)] = await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconSwordRoundAttack),
            [typeof(CritAttackSkill)] = await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconCritAttack),
            [typeof(SplashSkill)] = await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconSplash),
            [typeof(PassiveHealSkill)] = await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconPassiveHeal),
        };

        _lootIcons = new()
        {
            [LootType.Wood] = await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconSwordRoundAttack),
            [LootType.Diamond] = await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconSwordRoundAttack),
        };
    }

    public Sprite Get(Type skillType) => _skillsIcons[skillType];
}
