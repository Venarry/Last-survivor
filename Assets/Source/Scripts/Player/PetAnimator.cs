using UnityEngine;

public class PetAnimator : CharacterAnimator
{
    [SerializeField] private PetMovement _petMovement;

    protected override bool IsMoving => _petMovement.IsMoving;
}
