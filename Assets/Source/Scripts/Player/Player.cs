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
[RequireComponent(typeof(CharacterSkillsView))]
public class Player : MonoBehaviour
{
    private ThirdPersonMovement _thirdPersonMovement;
    private ThirdPersonRotation _thirdPersonRotation;
    private PlayerAttackStateMachine _playerAttackStateMachine;
    private ExperienceView _experienceView;
    private InventroyView _inventroyView;
    private PlayerHealthOverReaction _healthOverReaction;
    private CharacterSkillsView _characterSkillsView;

    public PlayerAttackHandler AttackHandler { get; private set; }
    public TargetSearcher TargetSearcher { get; private set; }
    public PlayerLootHolder LootHolder { get; private set; }
    public Target Target { get; private set; }
    public CharacterAttackParameters CharacterAttackParameters { get; private set; }

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
        _characterSkillsView = GetComponent<CharacterSkillsView>();

        _playerAttackStateMachine.Init(TargetSearcher, _thirdPersonRotation, AttackHandler, _playerAttackStateMachine);
    }

    public void Init(
        IInputProvider inputProvider,
        CharacterAttackParameters characterAttackParameters,
        TargetsProvider targetsProvider,
        InventoryModel inventoryModel,
        ExperienceModel experienceModel,
        HealthModel healthModel,
        CharacterSkillsModel characterSkillsModel,
        ItemViewFactory itemViewFactory,
        SkillsViewFactory skillsViewFactory,
        SpritesDataSouce spritesDataSouce,
        Transform lootParent,
        Transform skillsParent)
    {
        _thirdPersonMovement.Init(inputProvider);
        _thirdPersonRotation.Init(inputProvider);
        TargetSearcher.Init(targetsProvider);
        _inventroyView.Init(inventoryModel, itemViewFactory, spritesDataSouce, lootParent);
        _experienceView.Init(experienceModel);
        _healthOverReaction.Init(healthModel);
        Target.Init(TargetType.Enemy, healthModel);
        AttackHandler.Init(characterAttackParameters);
        _characterSkillsView.Init(characterSkillsModel, skillsViewFactory, skillsParent);

        CharacterAttackParameters = characterAttackParameters;
    }

    public void SetBehaviour(bool state)
    {
        _thirdPersonMovement.SetBehaviour(state);
    }
}
