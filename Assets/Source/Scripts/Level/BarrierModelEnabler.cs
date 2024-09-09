using UnityEngine;

public class BarrierModelEnabler : MonoBehaviour
{
    [SerializeField] private GameObject _barrier;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            _barrier.SetActive(true);
        }
    }
}
