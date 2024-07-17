using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        IInputProvider inputProvider;
        TargetsProvider targetsProvider = new();

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
    }
}
