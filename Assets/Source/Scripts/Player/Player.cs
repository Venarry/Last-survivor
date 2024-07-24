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
[RequireComponent(typeof(PlayerHealthOverReaction))]
public class Player : MonoBehaviour
{
    private ThirdPersonMovement _thirdPersonMovement;
    private ThirdPersonRotation _thirdPersonRotation;
    private TargetSearcher _targetSearcher;
    private PlayerAttackHandler _playerAttackHandler;
    private PlayerAttackStateMachine _playerAttackStateMachine;
    private ExperienceView _experienceView;
    private InventroyView _inventroyView;
    private PlayerHealthOverReaction _healthOverReaction;

    public PlayerLootHolder LootHolder { get; private set; }
    public Target Target { get; private set; }
    public CharacterAttackParameters CharacterAttackParameters { get; private set; }

    private void Awake()
    {
        _thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        _thirdPersonRotation = GetComponent<ThirdPersonRotation>();
        _targetSearcher = GetComponent<TargetSearcher>();
        _playerAttackHandler = GetComponent<PlayerAttackHandler>();
        _inventroyView = GetComponent<InventroyView>();
        _playerAttackStateMachine = GetComponent<PlayerAttackStateMachine>();
        _experienceView = GetComponent<ExperienceView>();
        _healthOverReaction = GetComponent<PlayerHealthOverReaction>();
        LootHolder = GetComponent<PlayerLootHolder>();
        Target = GetComponent<Target>();

        _playerAttackStateMachine.Init(_targetSearcher, _thirdPersonRotation, _playerAttackHandler, _playerAttackStateMachine);
    }

    public void Init(
        IInputProvider inputProvider,
        CharacterAttackParameters characterAttackParameters,
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
        _healthOverReaction.Init(healthModel);
        Target.Init(TargetType.Enemy, healthModel);
        _playerAttackHandler.Init(characterAttackParameters);

        CharacterAttackParameters = characterAttackParameters;
    }
}
