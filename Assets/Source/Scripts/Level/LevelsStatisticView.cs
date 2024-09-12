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
    }

    private void OnDestroy()
    {
        _levelsStatisticModel.Added -= OnLevelAdded;
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

    private void OnLevelAdded()
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
            _icons[_currentLevel].SetDectiveSize();
            _icons[_currentLevel].SetActiveColor();
        }

        UpdateActiveIcon();
    }

    private void SetComleteLevel(int index)
    {
        _icons[index].SetDectiveSize();
        _icons[index].SetActiveColor();
    }

    private void UpdateActiveIcon()
    {
        if(_levelsStatisticModel.CurrentLevel - _currentLevel > 1)
        {
            for (int i = _currentLevel; i < _levelsStatisticModel.CurrentLevel; i++)
            {
                SetComleteLevel(i);
            }
        }

        _currentLevel = _levelsStatisticModel.CurrentLevel;
        _icons[_currentLevel].SetActiveSize();
    }
}
