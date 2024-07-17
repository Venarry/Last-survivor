using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private void Awake()
    {
        bool isMobile = Application.isMobilePlatform;
        Debug.Log(isMobile);

        IInputProvider inputProvider = new KeyboardInputProvider();
        PlayerFactory playerFactory = new(inputProvider);

        playerFactory.Create(Vector3.zero);
    }
}
