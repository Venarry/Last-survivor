using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PetMovement))]
[RequireComponent(typeof(CharacterAttackHandler))]
public class PetBehaviour : MonoBehaviour
{
    private CharacterTargetSearcher _characterTargetSearcher;
    private CharacterAttackHandler _characterAttackHandler;
    private PetMovement _petMovement;
    private Target _currentTarget;
    private Coroutine _attackCoroutine;

    private void Awake()
    {
        _petMovement = GetComponent<PetMovement>();
        _characterAttackHandler = GetComponent<CharacterAttackHandler>();
    }

    public void Init(CharacterTargetSearcher characterTargetSearcher)
    {
        _characterTargetSearcher = characterTargetSearcher;
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
                _petMovement.Reached += OnTargetReach;
            }
        }
    }

    private void OnTargetEnd(Target target)
    {
        _currentTarget.LifeCycleEnded -= OnTargetEnd;
        _petMovement.Reached -= OnTargetReach;
        _currentTarget = null;

        if(_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }

        _petMovement.RemoveTarget();
    }

    private void OnTargetReach()
    {
        _attackCoroutine = StartCoroutine(Attacking());
    }

    private IEnumerator Attacking()
    {
        while (true)
        {
            if (_characterAttackHandler.ReadyToAttack == true)
            {
                _characterAttackHandler.TryAttack(_currentTarget);
            }

            yield return null;
        }
    }
}