using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    [SerializeField] private ThirdPersonMovement _thirdPersonMovement;

    protected override bool IsMoving => new Vector3(_thirdPersonMovement.Direction.x, 0, _thirdPersonMovement.Direction.z) != Vector3.zero;
}
