using System.Threading.Tasks;
using UnityEngine;

public class PetFactory
{
    private readonly AssetsProvider _assetsProvider;
    private readonly CharacterAttackParameters _characterAttackParameters;
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly CharacterTargetSearcher _characterTargetSearcher;
    private readonly Transform _followTarget;

    public PetFactory(
        AssetsProvider assetsProvider,
        CharacterAttackParameters characterAttackParameters,
        CharacterBuffsModel characterBuffsModel,
        CharacterTargetSearcher characterTargetSearcher,
        Transform followTarget)
    {
        _assetsProvider = assetsProvider;
        _characterAttackParameters = characterAttackParameters;
        _characterBuffsModel = characterBuffsModel;
        _characterTargetSearcher = characterTargetSearcher;
        _followTarget = followTarget;
    }

    public async Task Load()
    {
        await _assetsProvider.LoadGameObject<Pet>(AssetsKeys.Pet);
    }

    public async Task<Pet> Create(Vector3 position)
    {
        Pet prefab = await _assetsProvider.LoadGameObject<Pet>(AssetsKeys.Pet);
        Pet pet = Object.Instantiate(prefab, position, Quaternion.identity);
        pet.Init(_characterAttackParameters, _characterBuffsModel, _characterTargetSearcher, _followTarget);

        return pet;
    }
}