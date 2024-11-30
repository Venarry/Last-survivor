using System.Collections.Generic;
using Configs;
using UnityEngine;

namespace Level
{
    public class LevelsStatisticView : MonoBehaviour
    {
        private readonly List<LevelIcon> _icons = new ();

        [SerializeField] private Transform _levelsParent;
        [SerializeField] private LevelIcon _levelIconPrefab;

        private LevelsStatisticModel _levelsStatisticModel;
        private int _currentLevel;

        private void OnDestroy()
        {
            _levelsStatisticModel.Changed -= OnLevelChange;
        }

        public void Init(LevelsStatisticModel levelsStatisticModel)
        {
            _levelsStatisticModel = levelsStatisticModel;

            _levelsStatisticModel.Changed += OnLevelChange;
        }

        public void SpawnLevelsIcon()
        {
            int levelsToCheckpoint = GameParameters.LevelsForCheckpoint;
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
            if (_levelsStatisticModel.CurrentLevel == 0)
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
            if (_levelsStatisticModel.CurrentLevel >= _currentLevel)
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
}