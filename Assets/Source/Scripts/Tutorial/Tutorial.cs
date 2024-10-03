using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameTimeScaler _timeScaler;
    [SerializeField] private GameObject _startTutorialObject;
    [SerializeField] private List<TutorialPart> _tutorialParts;

    [SerializeField] private GameObject _moveTutorialBeginAction;
    [SerializeField] private GameObject _moveTutorialScreen;

    [SerializeField] private GameObject _shopTutorialScreen;

    private ThirdPersonMovement _thirdPersonMovement;

    public void InitBase()
    {
        for (int i = 0; i < _tutorialParts.Count; i++)
        {
            ActivateTutorial(_tutorialParts[i]);
        }

        foreach (GameObject tutorialObject in _tutorialParts[0].TutorialObjects)
        {
            tutorialObject.SetActive(true);
        }
    }

    public void InitMovement(ThirdPersonMovement thirdPersonMovement)
    {
        ITutorialAction beginAction = _moveTutorialBeginAction.GetComponent<ITutorialAction>();
        TutorialPart tutorialPart = new(beginAction, thirdPersonMovement, _moveTutorialScreen);
        _thirdPersonMovement = thirdPersonMovement;

        ActivateTutorial(tutorialPart);
    }

    public void InitShop(UpgradesShopTrigger upgradesShopTrigger, GameObject shopPoint)
    {
        ITutorialAction endAction = upgradesShopTrigger;

        TutorialPart tutorialPart = new(_thirdPersonMovement, endAction, _shopTutorialScreen, shopPoint);
        ActivateTutorial(tutorialPart);
    }

    public void BeginMovementTutorial()
    {
        _thirdPersonMovement.BeginMoveTutorial();
    }

    private void ActivateTutorial(TutorialPart tutorialPart)
    {
        if(_tutorialParts.Contains(tutorialPart) == false)
            _tutorialParts.Add(tutorialPart);

        if (tutorialPart.BeginAction != null)
            tutorialPart.BeginAction.Happened += OnBeginActionHappen;

        if (tutorialPart.EndAction != null)
            tutorialPart.EndAction.Happened += OnEndActionHappen;
    }

    private void OnBeginActionHappen(ITutorialAction tutorialAction)
    {
        tutorialAction.Happened -= OnBeginActionHappen;

        SetTutorialObjectsState(tutorialAction, true);
    }

    private void OnEndActionHappen(ITutorialAction tutorialAction)
    {
        tutorialAction.Happened -= OnEndActionHappen;

        SetTutorialObjectsState(tutorialAction, false);
    }

    private void SetTutorialObjectsState(ITutorialAction tutorialAction, bool state)
    {
        TutorialPart tutorialPart;

        if (state == true)
        {
            tutorialPart = _tutorialParts.FirstOrDefault(c => c.BeginAction == tutorialAction);
        }
        else
        {
            tutorialPart = _tutorialParts.FirstOrDefault(c => c.EndAction == tutorialAction);
        }

        foreach (GameObject tutorialObject in tutorialPart.TutorialObjects)
        {
            tutorialObject.SetActive(state);
        }
    }
}