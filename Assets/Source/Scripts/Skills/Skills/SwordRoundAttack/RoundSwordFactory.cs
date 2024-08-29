using System.Threading.Tasks;
using UnityEngine;

public class RoundSwordFactory
{
    private readonly CharacterAttackParameters _characterAttackParameters;
    private readonly AssetsProvider _assetsProvider;
    private RoundSword _roundSwordPrefab;

    public RoundSwordFactory(CharacterAttackParameters characterAttackParameters, AssetsProvider assetsProvider)
    {
        _characterAttackParameters = characterAttackParameters;
        _assetsProvider = assetsProvider;
    }

    public async void Load()
    {
        await _assetsProvider.LoadGameObject<RoundSword>(AssetsKeys.RoundSword);
    }

    public async Task<RoundSword> Create(Vector3 position, Transform target, int swordCount, float damageMultiplier, float swordScale)
    {
        _roundSwordPrefab = await _assetsProvider.LoadGameObject<RoundSword>(AssetsKeys.RoundSword);
        RoundSword roundSword = Object.Instantiate(_roundSwordPrefab, position, Quaternion.identity);

        roundSword.Init(_characterAttackParameters, target, swordCount, damageMultiplier, swordScale);
        return roundSword;
    }
}
