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
[RequireComponent(typeof(CharacterSkills))]
public class Player : MonoBehaviour
{
    private ThirdPersonMovement _thirdPersonMovement;
    private ThirdPersonRotation _thirdPersonRotation;
    private PlayerAttackStateMachine _playerAttackStateMachine;
    private ExperienceView _experienceView;
    private InventroyView _inventroyView;
    private PlayerHealthOverReaction _healthOverReaction;

    public PlayerAttackHandler AttackHandler { get; private set; }
    public TargetSearcher TargetSearcher { get; private set; }
    public PlayerLootHolder LootHolder { get; private set; }
    public Target Target { get; private set; }
    public CharacterAttackParameters CharacterAttackParameters { get; private set; }
    public CharacterSkills CharacterSkills { get; private set; }

    private void Awake()
    {
        _thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        _thirdPersonRotation = GetComponent<ThirdPersonRotation>();
        TargetSearcher = GetComponent<TargetSearcher>();
        AttackHandler = GetComponent<PlayerAttackHandler>();
        _inventroyView = GetComponent<InventroyView>();
        _playerAttackStateMachine = GetComponent<PlayerAttackStateMachine>();
        _experienceView = GetComponent<ExperienceView>();
        _healthOverReaction = GetComponent<PlayerHealthOverReaction>();
        LootHolder = GetComponent<PlayerLootHolder>();
        Target = GetComponent<Target>();
        CharacterSkills = GetComponent<CharacterSkills>();

        _playerAttackStateMachine.Init(TargetSearcher, _thirdPersonRotation, AttackHandler, _playerAttackStateMachine);
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
        TargetSearcher.Init(targetsProvider);
        _inventroyView.Init(inventoryModel);
        _experienceView.Init(experienceModel);
        _healthOverReaction.Init(healthModel);
        Target.Init(TargetType.Enemy, healthModel);
        AttackHandler.Init(characterAttackParameters);

        CharacterAttackParameters = characterAttackParameters;
    }

    public void SetBehaviour(bool state)
    {
        _thirdPersonMovement.SetBehaviour(state);
    }
}
