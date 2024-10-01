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

    public void Init(ThirdPersonMovement thirdPersonMovement) //params ITutorialAction[] tutorialActions
    {
        ITutorialAction moveBeginAction = _moveTutorialBeginAction.GetComponent<ITutorialAction>();
        TutorialPart movementTutorial = new(moveBeginAction, thirdPersonMovement, _moveTutorialScreen);
        _tutorialParts.Add(movementTutorial);

        for (int i = 0; i < _tutorialParts.Count; i++)
        {
            if(_tutorialParts[i].BeginAction != null)
                _tutorialParts[i].BeginAction.Happened += OnBeginActionHappen;

            if(_tutorialParts[i].EndAction != null)
                _tutorialParts[i].EndAction.Happened += OnEndActionHappen;
        }

        _tutorialParts[0].Screen.SetActive(true);

        thirdPersonMovement.BeginMoveTutorial();
    }

    private void OnEndActionHappen(ITutorialAction tutorialAction)
    {
        tutorialAction.Happened -= OnEndActionHappen;

        TutorialPart tutorialPart = _tutorialParts.FirstOrDefault(c => c.EndAction == tutorialAction);
        tutorialPart.Screen.SetActive(false);
    }

    private void OnBeginActionHappen(ITutorialAction tutorialAction)
    {
        tutorialAction.Happened -= OnBeginActionHappen;

        TutorialPart tutorialPart = _tutorialParts.FirstOrDefault(c => c.BeginAction == tutorialAction);
        tutorialPart.Screen.SetActive(true);
    }
}

[Serializable]
public class TutorialPart
{
    [SerializeField] private GameObject _beginTutorialAction;
    [SerializeField] private GameObject _screen;
    [SerializeField] private GameObject _endTutorialAction;
    [SerializeField] private bool _disableTime = false;

    private readonly ITutorialAction _beginAction;
    private readonly ITutorialAction _endAction;

    public GameObject Screen => _screen;

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

    public TutorialPart(ITutorialAction beginAction, ITutorialAction endAction, GameObject screen)
    {
        _beginAction = beginAction;
        _endAction = endAction;
        _screen = screen;
    }
}

public interface ITutorialAction
{
    public event Action<ITutorialAction> Happened;
}