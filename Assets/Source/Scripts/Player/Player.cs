using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ThirdPersonMovement))]
[RequireComponent(typeof(ThirdPersonRotation))]
[RequireComponent(typeof(InventroyView))]
[RequireComponent(typeof(CharacterAttackHandler))]
[RequireComponent(typeof(PlayerAttackStateMachine))]
[RequireComponent(typeof(ExperienceView))]
[RequireComponent(typeof(PlayerLootHolder))]
[RequireComponent(typeof(Target))]
[RequireComponent(typeof(PlayerHealthOverReaction))]
[RequireComponent(typeof(CharacterSkillsView))]
public class Player : MonoBehaviour
{
    [field: SerializeField] public GameObject DayBar { get; private set; }

    private ThirdPersonMovement _thirdPersonMovement;
    private ThirdPersonRotation _thirdPersonRotation;
    private PlayerAttackStateMachine _playerAttackStateMachine;
    private ExperienceView _experienceView;
    private InventroyView _inventroyView;
    private PlayerHealthOverReaction _healthOverReaction;
    private CharacterSkillsView _characterSkillsView;

    public CharacterAttackHandler AttackHandler { get; private set; }
    public CharacterTargetSearcher TargetSearcher { get; private set; }
    public PlayerLootHolder LootHolder { get; private set; }
    public Target Target { get; private set; }
    public CharacterAttackParameters CharacterAttackParameters { get; private set; }

    private void Awake()
    {
        _thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        _thirdPersonRotation = GetComponent<ThirdPersonRotation>();
        AttackHandler = GetComponent<CharacterAttackHandler>();
        _inventroyView = GetComponent<InventroyView>();
        _playerAttackStateMachine = GetComponent<PlayerAttackStateMachine>();
        _experienceView = GetComponent<ExperienceView>();
        _healthOverReaction = GetComponent<PlayerHealthOverReaction>();
        LootHolder = GetComponent<PlayerLootHolder>();
        Target = GetComponent<Target>();
        _characterSkillsView = GetComponent<CharacterSkillsView>();
    }

    public void Init(
        IInputProvider inputProvider,
        CharacterAttackParameters characterAttackParameters,
        TargetsProvider targetsProvider,
        InventoryModel inventoryModel,
        ExperienceModel experienceModel,
        HealthModel healthModel,
        CharacterBuffsModel characterBuffsModel,
        CharacterUpgradesModel<SkillBehaviour> characterSkillsModel,
        ItemViewFactory itemViewFactory,
        SkillsViewFactory skillsViewFactory,
        SpritesDataSouce spritesDataSouce,
        Transform lootParent,
        Transform skillsParent)
    {
        _thirdPersonMovement.Init(inputProvider);
        _thirdPersonRotation.Init(inputProvider);
        TargetSearcher = new(transform, targetsProvider);
        _inventroyView.Init(inventoryModel, itemViewFactory, spritesDataSouce, lootParent);
        _experienceView.Init(experienceModel);
        _healthOverReaction.Init(healthModel);
        Target.Init(TargetType.Enemy, healthModel);
        AttackHandler.Init(characterAttackParameters, characterBuffsModel);
        _characterSkillsView.Init(characterSkillsModel, skillsViewFactory, skillsParent);

        CharacterAttackParameters = characterAttackParameters;

        _playerAttackStateMachine.Init(TargetSearcher, _thirdPersonRotation, AttackHandler, _playerAttackStateMachine);
    }

    public void SetBehaviour(bool state)
    {
        _thirdPersonMovement.SetBehaviour(state);
    }
}
