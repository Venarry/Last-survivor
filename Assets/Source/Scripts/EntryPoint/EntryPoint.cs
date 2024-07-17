using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private void Awake()
    {
        IInputProvider inputProvider = new KeyboardInputProvider();
        PlayerFactory playerFactory = new(inputProvider);

        playerFactory.Create(Vector3.zero);
    }
}
