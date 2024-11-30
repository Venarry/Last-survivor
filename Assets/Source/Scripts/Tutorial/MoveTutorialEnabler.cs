using UnityEngine;

namespace GameTutorial
{
    public class MoveTutorialEnabler : MonoBehaviour
    {
        [SerializeField] private Tutorial _tutorial;

        private void OnEnable()
        {
            _tutorial.BeginMovementTutorial();
        }
    }
}