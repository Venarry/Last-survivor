using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SkillsSpriteDataSouce
{
    private Dictionary<Type, Sprite> _icons;

    public SkillsSpriteDataSouce()
    {
        Sprite swordRoundSkill;

        Addressables.LoadAssetAsync<Sprite>(ResourcesPath.SkillIconSwordRoundAttack)
            .Completed += (obj) =>
            {
                var swordRoundSkill = obj.Result;
                Debug.Log(swordRoundSkill);

                _icons = new() //Resources.Load<Sprite>(ResourcesPath.SkillIconSwordRoundAttack) 
                {
                    { typeof(SwordRoundAttackSkill), swordRoundSkill }
                };
            };

        //_icons = new() //Resources.Load<Sprite>(ResourcesPath.SkillIconSwordRoundAttack) 
        //{
        //    { typeof(SwordRoundAttackSkill), swordRoundSkill }
        //};
    }

    public Sprite Get(Type skillType)
    {
        return _icons[skillType];
    }
}
