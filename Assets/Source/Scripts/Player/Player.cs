using TMPro;
using UnityEngine;

[RequireComponent(typeof(ThirdPersonMovement))]
[RequireComponent(typeof(ThirdPersonRotation))]
[RequireComponent(typeof(CharacterAttackHandler))]
[RequireComponent(typeof(PlayerAttackStateMachine))]
[RequireComponent(typeof(ExperienceView))]
[RequireComponent(typeof(PlayerLootHolder))]
[RequireComponent(typeof(Target))]
[RequireComponent(typeof(CharacterSkillsView))]
public class Player : MonoBehaviour
{
    [field: SerializeField] public GameObject DayUIParent { get; private set; }
    [field: SerializeField] public Transform DayBar { get; private set; }
    [field: SerializeField] public TMP_Text DayTimeLabel { get; private set; }

    private ThirdPersonRotation _thirdPersonRotation;
    private PlayerAttackStateMachine _playerAttackStateMachine;
    private ExperienceView _experienceView;
    private InventroyView _inventroyView;
    private CharacterSkillsView _characterSkillsView;
    private PlayerHealthOverReaction _playerHealthOverReaction;

    public ThirdPersonMovement ThirdPersonMovement { get; private set; }
    public CharacterAttackHandler AttackHandler { get; private set; }
    public CharacterTargetSearcher TargetSearcher { get; private set; }
    public PlayerLootHolder LootHolder { get; private set; }
    public Target Target { get; private set; }

    private void Awake()
    {
        ThirdPersonMovement = GetComponent<ThirdPersonMovement>();
        _thirdPersonRotation = GetComponent<ThirdPersonRotation>();
        AttackHandler = GetComponent<CharacterAttackHandler>();
        _playerAttackStateMachine = GetComponent<PlayerAttackStateMachine>();
        _experienceView = GetComponent<ExperienceView>();
        LootHolder = GetComponent<PlayerLootHolder>();
        Target = GetComponent<Target>();
        _characterSkillsView = GetComponent<CharacterSkillsView>();
    }

    public void Init(
        IInputProvider inputProvider,
        CharacterAttackParameters characterAttackParameters,
        TargetsProvider<Target> targetsProvider,
        InventoryModel inventoryModel,
        ExperienceModel experienceModel,
        HealthModel healthModel,
        CharacterBuffsModel characterBuffsModel,
        CharacterUpgradesModel<SkillBehaviour> characterSkillsModel,
        ItemViewFactory itemViewFactory,
        SkillsViewFactory skillsViewFactory,
        SpritesDataSouce spritesDataSouce,
        Transform mainWindowInventoryParent,
        Transform shopInventoryParent,
        Transform skillsParent,
        GameRestartMenu deathMenu)
    {
        ThirdPersonMovement.Init(inputProvider);
        _thirdPersonRotation.Init(inputProvider);
        TargetSearcher = new(transform, targetsProvider);
        _inventroyView = new(inventoryModel, itemViewFactory, spritesDataSouce);
        _inventroyView.SpawnIcons(mainWindowInventoryParent, shopInventoryParent);
        _experienceView.Init(experienceModel);
        _characterSkillsView.Init(characterSkillsModel, skillsViewFactory, skillsParent);
        Target.Init(TargetType.Enemy, healthModel);
        AttackHandler.Init(characterAttackParameters, characterBuffsModel);
        LootHolder.Init(inventoryModel, experienceModel);

        _playerAttackStateMachine.Init(TargetSearcher, _thirdPersonRotation, AttackHandler, _playerAttackStateMachine);

        _playerHealthOverReaction = new(deathMenu, healthModel, gameObject);
        _playerHealthOverReaction.Enable();
    }

    public void SetBehaviour(bool state)
    {
        ThirdPersonMovement.SetBehaviour(state);
    }

    private void OnDestroy()
    {
        _playerHealthOverReaction.Disable();
    }
}
