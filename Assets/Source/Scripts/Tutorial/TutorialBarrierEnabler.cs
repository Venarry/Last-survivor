using UnityEngine;

public class TutorialBarrierEnabler : MonoBehaviour
{
    [SerializeField] private Collider _collider;

    private void OnEnable()
    {
        _collider.enabled = true;
    }
}
