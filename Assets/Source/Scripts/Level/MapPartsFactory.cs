using System.Threading.Tasks;
using UnityEngine;

public class MapPartsFactory
{
    private readonly AssetsProvider _assetsProvider;
    private readonly UpgradesShop _upgradesShop;

    public MapPartsFactory(
        AssetsProvider assetsProvider,
        UpgradesShop upgradesShop)
    {
        _assetsProvider = assetsProvider;
        _upgradesShop = upgradesShop;
    }

    public async Task Load()
    {
        await _assetsProvider.LoadGameObject<CheckpointPart>(AssetsKeys.CheckpointZone);
        await _assetsProvider.LoadGameObject<MapPart>(AssetsKeys.BetweenLevelsZone);
    }

    public async Task<MapPart> CreateCheckPointZone(Vector3 spawnPosition)
    {
        CheckpointPart part = Object.Instantiate(await _assetsProvider.LoadGameObject<CheckpointPart>(AssetsKeys.CheckpointZone), spawnPosition, Quaternion.identity);
        part.Init(_upgradesShop);

        return part;
    }

    public async Task<MapPart> CreateBetweenLevelZone(Vector3 spawnPosition) =>
        Object.Instantiate(await _assetsProvider.LoadGameObject<MapPart>(AssetsKeys.BetweenLevelsZone), spawnPosition, Quaternion.identity);
}