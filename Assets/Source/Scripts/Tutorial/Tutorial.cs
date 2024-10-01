using System;
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
    [SerializeField] private GameObject _shopPoint;

    private ITutorialAction _movementAction;

    public void InitBase() //params ITutorialAction[] tutorialActions
    {
        //ITutorialAction moveBeginAction = _moveTutorialBeginAction.GetComponent<ITutorialAction>();
        //TutorialPart movementTutorial = new(moveBeginAction, thirdPersonMovement, _moveTutorialScreen);
        //_tutorialParts.Add(movementTutorial);

        for (int i = 0; i < _tutorialParts.Count; i++)
        {
            //if(_tutorialParts[i].BeginAction != null)
            //    _tutorialParts[i].BeginAction.Happened += OnBeginActionHappen;

            //if(_tutorialParts[i].EndAction != null)
            //    _tutorialParts[i].EndAction.Happened += OnEndActionHappen;
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
        TutorialPart movementTutorial = new(beginAction, thirdPersonMovement, _moveTutorialScreen);
        _movementAction = thirdPersonMovement;

        ActivateTutorial(movementTutorial);
        thirdPersonMovement.BeginMoveTutorial();
    }

    public void InitShop(UpgradesShopTrigger upgradesShopTrigger)
    {
        ITutorialAction endAction = upgradesShopTrigger;

        TutorialPart tutorialPart = new(_movementAction, endAction, _shopTutorialScreen, _shopPoint);
        ActivateTutorial(tutorialPart);
    }

    private void ActivateTutorial(TutorialPart tutorialPart)
    {
        if (tutorialPart.BeginAction != null)
            tutorialPart.BeginAction.Happened += OnBeginActionHappen;

        if (tutorialPart.EndAction != null)
            tutorialPart.EndAction.Happened += OnEndActionHappen;
    }

    private void OnEndActionHappen(ITutorialAction tutorialAction)
    {
        tutorialAction.Happened -= OnEndActionHappen;

        SetTutorialObjectsState(tutorialAction, false);
    }

    private void OnBeginActionHappen(ITutorialAction tutorialAction)
    {
        tutorialAction.Happened -= OnBeginActionHappen;

        SetTutorialObjectsState(tutorialAction, true);
    }

    private void SetTutorialObjectsState(ITutorialAction tutorialAction, bool state)
    {
        TutorialPart tutorialPart = _tutorialParts.FirstOrDefault(c => c.BeginAction == tutorialAction);

        foreach (GameObject tutorialObject in tutorialPart.TutorialObjects)
        {
            tutorialObject.SetActive(state);
        }
    }
}

[Serializable]
public class TutorialPart
{
    [SerializeField] private string _name;
    [SerializeField] private GameObject _beginTutorialAction;
    [SerializeField] private List<GameObject> _tutorialObjects;
    [SerializeField] private GameObject _endTutorialAction;
    [SerializeField] private bool _disableTime = false;

    private readonly ITutorialAction _beginAction;
    private readonly ITutorialAction _endAction;

    public GameObject[] TutorialObjects => _tutorialObjects.ToArray();

    public ITutorialAction BeginAction
    {
        get
        {
            if(_beginAction != null)
            {
                return _beginAction;
            }

            if (_beginTutorialAction != null && _beginTutorialAction.TryGetComponent(out ITutorialAction tutorialAction))
            {
                return tutorialAction;
            }
            else
            {
                return null;
            }
        }
    }

    public ITutorialAction EndAction
    {
        get
        {
            if (_endAction != null)
            {
                return _endAction;
            }

            if (_endTutorialAction != null && _endTutorialAction.TryGetComponent(out ITutorialAction tutorialAction))
            {
                return tutorialAction;
            }
            else
            {
                return null;
            }
        }
    }

    public TutorialPart(ITutorialAction beginAction, ITutorialAction endAction, params GameObject[] screen)
    {
        _beginAction = beginAction;
        _endAction = endAction;
        _tutorialObjects = screen.ToList();
    }
}

public interface ITutorialAction
{
    public event Action<ITutorialAction> Happened;
}