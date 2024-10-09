using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public TutorialPart(string name, bool disableTime, ITutorialAction beginAction, ITutorialAction endAction, GameObject[] screen)
    {
        _name = name;
        _beginAction = beginAction;
        _endAction = endAction;
        _disableTime = disableTime;
        _tutorialObjects = screen.ToList();
    }

    public GameObject[] TutorialObjects => _tutorialObjects.ToArray();
    public string Name => _name;
    public bool DisableTime => _disableTime;

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
}
