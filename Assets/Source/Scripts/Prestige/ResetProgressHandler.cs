using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetProgressHandler : MonoBehaviour
{
    [SerializeField] private Button _resetProgressButton;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private GameObject _resetProgressMenu;
    [SerializeField] private MapGenerator _mapGenerator;

    private LevelsStatisticModel _levelsStatisticModel;
    private InventoryModel _characterInventory;
    private CharacterUpgradesModel<ParametersUpgradeBehaviour> _characterUpgrades;
    private ThirdPersonMovement _thirdPersonMovement;
    private Vector3 _respawnPosition;

    public void Init(
        LevelsStatisticModel levelsStatisticModel,
        InventoryModel characterInventory,
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterUpgrades,
        ThirdPersonMovement thirdPersonMovement,
        Vector3 respawnPosition)
    {
        _levelsStatisticModel = levelsStatisticModel;
        _characterInventory = characterInventory;
        _characterUpgrades = characterUpgrades;
        _thirdPersonMovement = thirdPersonMovement;
        _respawnPosition = respawnPosition;
    }

    private void OnEnable()
    {
        _resetProgressButton.onClick.AddListener(OpenResetProgressMenu);
        _confirmButton.onClick.AddListener(ResetProgress);
    }

    private void OnDisable()
    {
        _resetProgressButton.onClick.RemoveListener(OpenResetProgressMenu);
        _confirmButton.onClick.RemoveListener(ResetProgress);
    }

    private void OpenResetProgressMenu()
    {
        _resetProgressMenu.SetActive(true);
    }

    private void ResetProgress()
    {
        _levelsStatisticModel.Set(0);
        _thirdPersonMovement.SetPosition(_respawnPosition);
        _characterUpgrades.RemoveAll();
        _mapGenerator.ResetLevels();
        _characterInventory.RemoveWithNotIncluding(new List<LootType>() { LootType.Prestige });
        _resetProgressMenu.SetActive(false);
    }
}
