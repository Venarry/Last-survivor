using UnityEngine;
using UnityEngine.UI;

public class MapLevelsStatisticView : MonoBehaviour
{
    [SerializeField] private Image _checkPointBar;

    private LevelsStatisticModel _levelsStatistic;

    public void Init(LevelsStatisticModel levelsStatistic)
    {
        _levelsStatistic = levelsStatistic;
    }

    public void Add()
    {
        _levelsStatistic.Add();
    }
}
