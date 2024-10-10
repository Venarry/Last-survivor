using UnityEngine;

public class TutorialBarrierDisabler : MonoBehaviour
{
    [SerializeField] private GameObject _barrier;

    private void OnDisable()
    {
        _barrier.SetActive(false);
    }
}
