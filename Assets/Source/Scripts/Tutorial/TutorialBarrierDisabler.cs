using UnityEngine;

public class TutorialBarrierDisabler : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    private void OnDisable()
    {
        _collider.enabled = false;
    }
}
