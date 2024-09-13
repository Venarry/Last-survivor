using System.Threading.Tasks;
using UnityEngine;

public class ThrowingAxesFactory
{
    private readonly AssetsProvider _assetsProvider;
    private readonly CharacterAttackParameters _characterAttackParameters;

    public ThrowingAxesFactory(AssetsProvider assetsProvider, CharacterAttackParameters characterAttackParameters)
    {
        _assetsProvider = assetsProvider;
        _characterAttackParameters = characterAttackParameters;
    }

    public async Task Load()
    {
        await _assetsProvider.LoadGameObject<ThrowingAxe>(AssetsKeys.ThrowingAxe);
    }

    public async Task<ThrowingAxe> Create(Vector3 position, float throwDistance, Transform owner, float damageMultiplier)
    {
        ThrowingAxe prefab = await _assetsProvider.LoadGameObject<ThrowingAxe>(AssetsKeys.ThrowingAxe);
        ThrowingAxe throwingAxe = Object.Instantiate(prefab, position, Quaternion.identity);
        throwingAxe.Init(throwDistance, owner, _characterAttackParameters, damageMultiplier);

        return throwingAxe;
    }
}