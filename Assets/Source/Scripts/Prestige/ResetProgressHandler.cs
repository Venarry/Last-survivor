using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetProgressHandler : MonoBehaviour
{
    [SerializeField] private Button _resetProgressButton;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;
    [SerializeField] private TMP_Text _prestigeToAddCountLabel;
    [SerializeField] private GameObject _resetProgressMenu;
    [SerializeField] private MapGenerator _mapGenerator;
    [SerializeField] private UpgradesShop _upgradesShop;

    private LevelsStatisticModel _levelsStatisticModel;
    private InventoryModel _characterInventory;
    private CharacterUpgradesModel<ParametersUpgradeBehaviour> _characterUpgrades;
    private ThirdPersonMovement _thirdPersonMovement;
    private IProgressSaveService _progressSaveService;
    private Vector3 _respawnPosition;

    private int PrestigeToAdd => Mathf.FloorToInt(Mathf.Pow(_levelsStatisticModel.TotalLevel, 1.1f));

    public void Init(
        LevelsStatisticModel levelsStatisticModel,
        InventoryModel characterInventory,
        CharacterUpgradesModel<ParametersUpgradeBehaviour> characterUpgrades,
        ThirdPersonMovement thirdPersonMovement,
        IProgressSaveService progressSaveService,
        Vector3 respawnPosition)
    {
        _levelsStatisticModel = levelsStatisticModel;
        _characterInventory = characterInventory;
        _characterUpgrades = characterUpgrades;
        _thirdPersonMovement = thirdPersonMovement;
        _progressSaveService = progressSaveService;
        _respawnPosition = respawnPosition;

        _resetProgressMenu.SetActive(false);
    }

    private void OnEnable()
    {
        _resetProgressButton.onClick.AddListener(OpenResetProgressMenu);
        _confirmButton.onClick.AddListener(ResetProgress);
        _cancelButton.onClick.AddListener(Hide);
    }


    private void OnDisable()
    {
        _resetProgressButton.onClick.RemoveListener(OpenResetProgressMenu);
        _confirmButton.onClick.RemoveListener(ResetProgress);
        _cancelButton.onClick.RemoveListener(Hide);
    }

    private void OpenResetProgressMenu()
    {
        _resetProgressMenu.SetActive(true);

        int minLevelForResetPreogress = 10;

        if(_levelsStatisticModel.TotalLevel >= minLevelForResetPreogress)
        {
            _prestigeToAddCountLabel.text = $"You'll get {PrestigeToAdd} prestige coins";
            _confirmButton.gameObject.SetActive(true);
        }
        else
        {
            _prestigeToAddCountLabel.text = $"You need {minLevelForResetPreogress} level or highter for reset progress";
            //_confirmButton.gameObject.SetActive(false);
        }
    }

    private void ResetProgress()
    {
        _characterInventory.Add(LootType.Prestige, PrestigeToAdd);

        _levelsStatisticModel.Set(0);
        _thirdPersonMovement.SetPosition(_respawnPosition);
        _characterUpgrades.RemoveAll();
        _mapGenerator.ResetLevels();
        _characterInventory.RemoveWithNotIncluding(new List<LootType>() { LootType.Prestige });

        Hide();
        _upgradesShop.Hide();

        _progressSaveService.Save();
        _progressSaveService.ReloadShop();
    }

    private void Hide()
    {
        _resetProgressMenu.SetActive(false);
    }
}
