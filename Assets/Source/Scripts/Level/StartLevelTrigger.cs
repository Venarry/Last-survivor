using UnityEngine;

public class StartLevelTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider _startLevelCollider;
    private DayCycle _dayCycle;

    public void Init(DayCycle dayCycle)
    {
        _dayCycle = dayCycle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player _))
        {
            _dayCycle.StartDayTimer();
            _startLevelCollider.enabled = true;
            Destroy(this);
        }
    }
}
