using UnityEngine;

public class PlayerHealthOverReaction
{
    private readonly GameRestartMenu _deathMenu;
    private readonly HealthModel _healthModel;
    private readonly GameObject _gameObject;

    public PlayerHealthOverReaction(
        GameRestartMenu deathMenu,
        HealthModel healthModel,
        GameObject gameObject)
    {
        _deathMenu = deathMenu;
        _healthModel = healthModel;
        _gameObject = gameObject;
    }

    public void Enable()
    {
        _healthModel.HealthOver += OnHealthOver;
    }

    public void Disable()
    {
        _healthModel.HealthOver -= OnHealthOver;
    }

    private void OnHealthOver()
    {
        _deathMenu.Show();
        _gameObject.SetActive(false);
    }
}
