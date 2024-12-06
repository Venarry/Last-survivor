using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private TMP_Text _healthLabel;

        private HealthModel _healthModel;

        public event Action HealthOver;

        public void Init(HealthModel healthModel)
        {
            _healthModel = healthModel;

            _healthModel.Changed += OnHealthChange;
            _healthModel.HealthOver += OnHealthOver;

            OnHealthChange();
        }

        private void OnHealthChange()
        {
            _healthBar.fillAmount = _healthModel.HealthNormalized;
            _healthLabel.text = $"{Math.Round(_healthModel.Value, 1)}/{_healthModel.MaxValue}";
        }

        private void OnHealthOver()
        {
            HealthOver?.Invoke();
        }
    }
}