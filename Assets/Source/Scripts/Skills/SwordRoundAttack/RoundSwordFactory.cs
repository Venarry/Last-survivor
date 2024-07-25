using UnityEngine;

public class RoundSwordFactory
{
    private readonly RoundSword _roundSwordPrefab = Resources.Load<RoundSword>(ResourcesPath.RoundSword);
    private readonly CharacterAttackParameters _characterAttackParameters;

    public RoundSwordFactory(CharacterAttackParameters characterAttackParameters)
    {
        _characterAttackParameters = characterAttackParameters;
    }

    public RoundSword Create(Vector3 position, Transform target)
    {
        RoundSword roundSword = Object.Instantiate(_roundSwordPrefab, position, Quaternion.identity);

        roundSword.Init(_characterAttackParameters, target);
        return roundSword;
    }
}
