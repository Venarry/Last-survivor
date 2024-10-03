using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTutorialEnabler : MonoBehaviour
{
    [SerializeField] private Tutorial _tutorial;

    private void OnEnable()
    {
        _tutorial.BeginMovementTutorial();
    }
}
