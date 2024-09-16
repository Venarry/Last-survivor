using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _healthBar;

    private DamageIndicator _damageIndicator;
    private HealthModel _healthModel;

    public event Action HealthOver;

    private void Awake()
    {
        _damageIndicator = GetComponent<DamageIndicator>();
    }

    public void Init(HealthModel healthModel)
    {
        _healthModel = healthModel;

        _healthModel.Changed += OnHealthChange;
        _healthModel.HealthOver += OnHealthOver;
        _healthModel.DamageReceived += OnDamageReceive;

        OnHealthChange();
    }

    private void OnDamageReceive()
    {
        _damageIndicator?.Shake();
    }

    public void TakeDamage(float count)
    {
        _healthModel.TakeDamage(count);
    }

    public void Restore()
    {
        _healthModel.Restore();
    }

    public void SetMaxHealth(float health)
    {
        _healthModel.SetMaxHealth(health);
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
