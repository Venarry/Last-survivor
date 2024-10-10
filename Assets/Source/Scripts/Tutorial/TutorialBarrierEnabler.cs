using UnityEngine;

public class TutorialBarrierEnabler : MonoBehaviour
{
    [SerializeField] private GameObject _barrier;

    private void OnEnable()
    {
        _barrier.SetActive(true);
    }
}
