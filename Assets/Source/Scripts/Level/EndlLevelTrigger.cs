using UnityEngine;

public class EndlLevelTrigger : MonoBehaviour
{
    private LevelsStatisticModel _levelsStatisticModel;

    public void Init(LevelsStatisticModel levelsStatisticModel)
    {
        _levelsStatisticModel = levelsStatisticModel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player _))
        {
            _levelsStatisticModel.Add();
            Destroy(this);
        }
    }
}
