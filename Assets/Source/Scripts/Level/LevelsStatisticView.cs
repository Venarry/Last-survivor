using System;
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

        _levelsStatisticModel.Changed += OnLevelChange;
    }

    private void OnDestroy()
    {
        _levelsStatisticModel.Changed -= OnLevelChange;
    }

    public void SpawnLevelsIcon()
    {
        int levelsToCheckpoint = GameParamenters.LevelsForCheckpoint;
        int startLevelCounter = _levelsStatisticModel.TotalLevel - _levelsStatisticModel.CurrentLevel;

        for (int i = 0; i < levelsToCheckpoint; i++)
        {
            LevelIcon levelIcon = Instantiate(_levelIconPrefab, _levelsParent);
            levelIcon.SetLevelNumber(startLevelCounter + i);

            _icons.Add(levelIcon);
        }

        UpdateActiveIcon();
    }

    private void OnLevelChange()
    {
        if(_levelsStatisticModel.CurrentLevel == 0)
        {
            _icons[_currentLevel].SetDectiveSize();

            int startLevelCounter = _levelsStatisticModel.TotalLevel;

            for (int i = 0; i < _icons.Count; i++)
            {
                _icons[i].SetLevelNumber(startLevelCounter + i);
                _icons[i].SetDectiveColor();
            }
        }
        else
        {
            SetComleteLevelView(_currentLevel);
        }

        UpdateActiveIcon();
    }

    private void UpdateActiveIcon()
    {
        if(_levelsStatisticModel.CurrentLevel >= _currentLevel)
        {
            for (int i = _currentLevel; i < _levelsStatisticModel.CurrentLevel; i++)
            {
                SetComleteLevelView(i);
            }
        }
        else
        {
            for (int i = _levelsStatisticModel.CurrentLevel; i < _currentLevel; i++)
            {
                _icons[i].SetDectiveSize();
                _icons[i].SetDectiveColor();
            }
        }

        _currentLevel = _levelsStatisticModel.CurrentLevel;
        _icons[_currentLevel].SetActiveSize();
    }

    private void SetComleteLevelView(int index)
    {
        _icons[index].SetDectiveSize();
        _icons[index].SetActiveColor();
    }
}
