using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        IInputProvider inputProvider;
        TargetsProvider targetsProvider = new();
        MineralsFactory mineralsFactory = new(targetsProvider);

        bool isMobile = Application.isMobilePlatform;

        if (isMobile == false)
        {
            inputProvider = new KeyboardInputProvider();
        }
        else
        {
            MobileInputsProviderFactory mobileInputsProviderFactory = new MobileInputsProviderFactory();
            inputProvider = mobileInputsProviderFactory.Create(_canvas.transform);
        }

        PlayerFactory playerFactory = new(inputProvider, targetsProvider);

        playerFactory.Create(Vector3.zero);

        mineralsFactory.Create(new Vector3(3, 0, 3), Quaternion.identity);
    }
}
