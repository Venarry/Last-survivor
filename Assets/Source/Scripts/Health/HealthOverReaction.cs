using UnityEngine;

public abstract class HealthOverReaction : MonoBehaviour
{
    private HealthModel _healthModel;

    public void Init(HealthModel healthModel)
    {
        _healthModel = healthModel;

        _healthModel.HealthOver += OnHealthOver;
    }

    private void OnDestroy()
    {
        _healthModel.HealthOver -= OnHealthOver;
    }

    protected abstract void OnHealthOver();
}