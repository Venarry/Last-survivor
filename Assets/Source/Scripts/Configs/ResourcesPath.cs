using UnityEngine;

public class ResourcesPath
{
    private const string PrefabsPath = "Prefabs/";
    private const string SpritesPath = "";

    public const string Player = /*PrefabsPath + */"Player";
    public const string MobileInputsProvider = PrefabsPath + "FloatingJoystick";
    public const string Diamond = PrefabsPath + "Diamond";
    public const string Wood = PrefabsPath + "Wood";
    public const string Enemy = PrefabsPath + "Enemy";
    public const string DiamondLoot = PrefabsPath + "DiamondLoot";
    public const string WoodLoot = PrefabsPath + "WoodLoot";

    public const string RoundSword = PrefabsPath + "RoundSword";

    public const string SkillIconSwordRoundAttack = SpritesPath + "SkillIconSwordRoundAttack";
}
