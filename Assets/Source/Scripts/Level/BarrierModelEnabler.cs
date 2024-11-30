using Player;
using UnityEngine;

namespace Level
{
    public class BarrierModelEnabler : MonoBehaviour
    {
        [SerializeField] private GameObject _barrier;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerCompositeRoot _))
            {
                _barrier.SetActive(true);
            }
        }
    }
}