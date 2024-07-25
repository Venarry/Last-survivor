public class EnemyDamageUpgrade : IUpgrade
{
    private readonly CharacterAttackParameters _characterAttackParameters;
    private int _damagePerLevel = 5;
    private int _currentLevel = 0;

    public EnemyDamageUpgrade(CharacterAttackParameters characterAttackParameters)
    {
        _characterAttackParameters = characterAttackParameters;
    }

    public void Apply()
    {
        _characterAttackParameters.EnemyDamage += _damagePerLevel * _currentLevel;
    }

    public void Cancel()
    {
        _characterAttackParameters.EnemyDamage -= _damagePerLevel * _currentLevel;
    }

    public void IncreaseLevel()
    {
        if(_currentLevel != 0)
        {
            Cancel();
        }

        _currentLevel++;

        Apply();
    }
}
