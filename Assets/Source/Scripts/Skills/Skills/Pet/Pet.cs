using UnityEngine;

[RequireComponent(typeof(CharacterAttackHandler))]
[RequireComponent(typeof(PetBehaviour))]
[RequireComponent(typeof(PetMovement))]
public class Pet : MonoBehaviour
{
    private CharacterAttackHandler _characterAttackHandler;
    private PetMovement _petMovement;
    private PetBehaviour _petBehaviour;

    private void Awake()
    {
        _characterAttackHandler = GetComponent<CharacterAttackHandler>();
        _petMovement = GetComponent<PetMovement>();
        _petBehaviour = GetComponent<PetBehaviour>();
    }

    public void Init(
        CharacterAttackParameters characterAttackParameters,
        CharacterBuffsModel characterBuffsModel,
        CharacterTargetSearcher characterTargetSearcher,
        Transform folllowTarget)
    {
        _characterAttackHandler.Init(characterAttackParameters, characterBuffsModel);
        _petMovement.Init(folllowTarget);
        _petBehaviour.Init(characterTargetSearcher);
    }

    public void SetParameters(float attackDamageMultiplier, float attackCooldownMultiplier, float moveToTargetDelay)
    {
        _characterAttackHandler.SetParameters(attackDamageMultiplier, attackCooldownMultiplier);
        _petMovement.SetParameters(moveToTargetDelay);
    }
}
