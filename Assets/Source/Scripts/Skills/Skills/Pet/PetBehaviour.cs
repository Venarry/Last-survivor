using System;
using UnityEngine;

[RequireComponent(typeof(PetMovement))]
public class PetBehaviour : MonoBehaviour
{
    private CharacterTargetSearcher _characterTargetSearcher;
    private PetMovement _petMovement;
    private Target _currentTarget;

    private void Awake()
    {
        _petMovement = GetComponent<PetMovement>();
    }

    public void Init(CharacterTargetSearcher characterTargetSearcher, Target followTarget)
    {
        _characterTargetSearcher= characterTargetSearcher;

        _petMovement.Init(followTarget);
    }

    private void Update()
    {
        if (_characterTargetSearcher == null)
            return;

        if(_characterTargetSearcher.TryGetNearestTarget(out Target target) == true)
        {
            if(_currentTarget != target)
            {
                if(_currentTarget != null)
                {
                    OnTargetEnd(_currentTarget);
                }

                _petMovement.GoTo(target.Position);

                _currentTarget = target;
                _currentTarget.LifeCycleEnded += OnTargetEnd;
            }
        }
    }

    private void OnTargetEnd(Target target)
    {
        _currentTarget.LifeCycleEnded -= OnTargetEnd;
        _currentTarget = null;

        _petMovement.RemoveTarget();
    }
}