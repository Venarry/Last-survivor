using System.Collections.Generic;
using UnityEngine;

public class LevelsStatisticView : MonoBehaviour
{
    [SerializeField] private Transform _levelsParent;
    [SerializeField] private LevelIcon _levelIconPrefab;

    private readonly List<LevelIcon> _icons = new();
    private LevelsStatisticModel _levelsStatisticModel;
    private int _currentLevel;

    public void Init(LevelsStatisticModel levelsStatisticModel)
    {
        _levelsStatisticModel = levelsStatisticModel;

        _levelsStatisticModel.Added += OnLevelAdded;
        SpawnLevelsIcon();
    }

    private void OnDestroy()
    {
        _levelsStatisticModel.Added -= OnLevelAdded;
    }

    private void SpawnLevelsIcon()
    {
        int levelsToCheckpoint = GameParamenters.LevelsForCheckpoint;
        int startLevelCounter = _levelsStatisticModel.TotalLevel - _levelsStatisticModel.CurrentLevel;

        for (int i = 0; i < levelsToCheckpoint; i++)
        {
            LevelIcon levelIcon = Instantiate(_levelIconPrefab, _levelsParent);
            levelIcon.SetLevelNumber(startLevelCounter + i);

            _icons.Add(levelIcon);
        }

        RefreshActiveIcon();
    }

    private void OnLevelAdded()
    {
        if(_levelsStatisticModel.CurrentLevel == 0)
        {
            _icons[_currentLevel].SetDectiveSize();

            int startLevelCounter = _levelsStatisticModel.TotalLevel - _levelsStatisticModel.CurrentLevel;

            for (int i = 0; i < _icons.Count; i++)
            {
                _icons[i].SetLevelNumber(startLevelCounter + i);
                _icons[i].SetDectiveColor();
            }

            RefreshActiveIcon();
        }
        else
        {
            _icons[_currentLevel].SetDectiveSize();
            _icons[_currentLevel].SetActiveColor();

            RefreshActiveIcon();
        }
    }

    private void RefreshActiveIcon()
    {
        _currentLevel = _levelsStatisticModel.CurrentLevel;
        _icons[_currentLevel].SetActiveSize();
    }
}
