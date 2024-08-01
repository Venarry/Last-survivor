using System.Threading.Tasks;
using UnityEngine;

public class MobileInputsProviderFactory
{
    private readonly AssetsProvider _assetsProvider;
    private MobileInputsProvider _mobileInputsProviderPrefab;

    public MobileInputsProviderFactory(AssetsProvider assetsProvider)
    {
        _assetsProvider = assetsProvider;
    }

    public async Task<MobileInputsProvider> Create(Transform parent)
    {
        _mobileInputsProviderPrefab = await _assetsProvider.LoadGameObject<MobileInputsProvider>(AssetsKeys.MobileInputsProvider);
        MobileInputsProvider mobileInputsProvider = Object.Instantiate(_mobileInputsProviderPrefab, parent);
        mobileInputsProvider.transform.SetSiblingIndex(0);

        return mobileInputsProvider;
    }
}
