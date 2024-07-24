using UnityEngine;

[RequireComponent(typeof(ThirdPersonMovement))]
[RequireComponent(typeof(ThirdPersonRotation))]
[RequireComponent(typeof(TargetSearcher))]
[RequireComponent(typeof(InventroyView))]
[RequireComponent(typeof(PlayerAttackHandler))]
[RequireComponent(typeof(PlayerAttackStateMachine))]
[RequireComponent(typeof(ExperienceView))]
[RequireComponent(typeof(PlayerLootHolder))]
[RequireComponent(typeof(Target))]
public class Player : MonoBehaviour
{
    private ThirdPersonMovement _thirdPersonMovement;
    private ThirdPersonRotation _thirdPersonRotation;
    private TargetSearcher _targetSearcher;
    private PlayerAttackHandler _playerAttackHandler;
    private PlayerAttackStateMachine _playerAttackStateMachine;
    private ExperienceView _experienceView;
    private InventroyView _inventroyView;
    private Target _target;

    public PlayerLootHolder LootHolder { get; private set; }

    private void Awake()
    {
        _thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        _thirdPersonRotation = GetComponent<ThirdPersonRotation>();
        _targetSearcher = GetComponent<TargetSearcher>();
        _playerAttackHandler = GetComponent<PlayerAttackHandler>();
        _inventroyView = GetComponent<InventroyView>();
        _playerAttackStateMachine = GetComponent<PlayerAttackStateMachine>();
        _experienceView = GetComponent<ExperienceView>();
        LootHolder = GetComponent<PlayerLootHolder>();
        _target = GetComponent<Target>();

        _playerAttackStateMachine.Init(_targetSearcher, _thirdPersonRotation, _playerAttackHandler, _playerAttackStateMachine);
    }

    public void Init(
        IInputProvider inputProvider,
        TargetsProvider targetsProvider,
        InventoryModel inventoryModel,
        ExperienceModel experienceModel,
        HealthModel healthModel)
    {
        _thirdPersonMovement.Init(inputProvider);
        _thirdPersonRotation.Init(inputProvider);
        _targetSearcher.Init(targetsProvider);
        _inventroyView.Init(inventoryModel);
        _experienceView.Init(experienceModel);
        _target.Init(TargetType.Enemy, healthModel);
    }
}
