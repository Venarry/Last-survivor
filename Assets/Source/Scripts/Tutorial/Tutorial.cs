using System;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameTimeScaler _timeScaler;
    [SerializeField] private List<GameObject> _screens;

    private List<TutorialPart> _tutorialParts;
    private int _partsCounter = 0;

    public void Init(params ITutorialAction[] tutorialActions)
    {
        int partsCount = Math.Min(tutorialActions.Length, _screens.Count);
        _tutorialParts = new();

        for (int i = 0; i < partsCount; i++)
        {
            _tutorialParts.Add(new(tutorialActions[i], _screens[i]));
            _screens[i].SetActive(false);
        }
    }

    public void ShowNext()
    {
        if (_partsCounter > 0)
        {
            _tutorialParts[_partsCounter - 1].Screen.SetActive(false);
        }

        if(_partsCounter == _tutorialParts.Count)
        {


            return;
        }

        _tutorialParts[_partsCounter].Screen.SetActive(true);
        _tutorialParts[_partsCounter].Action.Happened += OnActionHappen;
    }

    private void OnActionHappen(ITutorialAction tutorialAction)
    {
        tutorialAction.Happened -= OnActionHappen;

        _partsCounter++;
        ShowNext();
    }
}

public class TutorialPart
{
    public readonly ITutorialAction Action;
    public readonly GameObject Screen;

    public TutorialPart(ITutorialAction action, GameObject screen)
    {
        Action = action;
        Screen = screen;
    }
}

public interface ITutorialAction
{
    public event Action<ITutorialAction> Happened;
}
