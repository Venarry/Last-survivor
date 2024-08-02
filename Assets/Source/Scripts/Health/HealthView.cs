using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    private HealthModel _healthModel;

    public event Action HealthOver;

    public void Init(HealthModel healthModel)
    {
        _healthModel = healthModel;

        _healthModel.HealthChanged += OnHealthChange;
        _healthModel.HealthOver += OnHealthOver;

        OnHealthChange();
    }

    public void TakeDamage(float count)
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
        HealthOver?.Invoke();
    }
}
