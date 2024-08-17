using UnityEngine;

public class EndlLevelTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider _endLevelCollider;

    private LevelsStatisticModel _levelsStatisticModel;
    private DayCycle _dayCycle;
    private bool _isEnabled = false;

    public void Init(DayCycle dayCycle, LevelsStatisticModel levelsStatisticModel)
    {
        _dayCycle = dayCycle;
        _levelsStatisticModel = levelsStatisticModel;
        _isEnabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isEnabled == false)
            return;

        if(other.TryGetComponent(out Player _))
        {
            _levelsStatisticModel.Add();
            _dayCycle.ResetTime();
            _endLevelCollider.enabled = true;
            Destroy(this);
        }
    }
}
