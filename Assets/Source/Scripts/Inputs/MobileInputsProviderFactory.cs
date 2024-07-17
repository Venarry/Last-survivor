using UnityEngine;

public class MobileInputsProviderFactory
{
    private readonly MobileInputsProvider _mobileInputsProviderPrefab = 
        Resources.Load<MobileInputsProvider>(ResourcesPath.MobileInputsProvider);

    public MobileInputsProvider Create(Transform parent)
    {
        MobileInputsProvider mobileInputsProvider = Object.Instantiate(_mobileInputsProviderPrefab, parent);

        return mobileInputsProvider;
    }
}
