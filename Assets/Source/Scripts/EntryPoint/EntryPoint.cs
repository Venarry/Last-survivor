using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private void Awake()
    {
        bool isMobile = Application.isMobilePlatform;

        IInputProvider inputProvider;

        if (isMobile == false)
        {
            inputProvider = new KeyboardInputProvider();
        }
        else
        {
            MobileInputsProviderFactory mobileInputsProviderFactory = new MobileInputsProviderFactory();
            inputProvider = mobileInputsProviderFactory.Create(_canvas.transform);
        }

        PlayerFactory playerFactory = new(inputProvider);

        playerFactory.Create(Vector3.zero);
    }
}
