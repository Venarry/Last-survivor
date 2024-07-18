using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    private HealthModel _healthModel;

    public void Init(HealthModel healthModel)
    {
        _healthModel = healthModel;

        _healthModel.HealthChanged += OnHealthChange;
        _healthModel.HealthOver += OnHealthOver;
    }

    public void TakeDamage(int count)
    {
        _healthModel.TakeDamage(count);
    }

    public void Restore()
    {
        _healthModel.Restore();
    }

    private void OnHealthChange()
    {
        _healthBar.fillAmount = _healthModel.HealthNormalized;
    }

    private void OnHealthOver()
    {
    }
}