using System;
using System.Collections.Generic;
using System.Linq;
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
            [typeof(AttackSpeedSkill)] = await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconAttackSpeed),
            [typeof(MaxHealthUpSkill)] = await _assetProvider.Load<Sprite>(AssetsKeys.SkillIconMaxHealtUp),
        };

        _lootIcons = new()
        {
            [LootType.Wood] = await _assetProvider.Load<Sprite>(AssetsKeys.ItemWood),
            [LootType.Diamond] = await _assetProvider.Load<Sprite>(AssetsKeys.ItemDiamond),
        };
    }

    public Sprite Get(Type skillType) => _skillsIcons[skillType];
    public Sprite Get(LootType lootType) => _lootIcons[lootType];
    public Dictionary<LootType, Sprite> GetAllItemsIcon() => _lootIcons.ToDictionary(c => c.Key, x => x.Value);
}
