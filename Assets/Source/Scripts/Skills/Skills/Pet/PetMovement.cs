using System;
using System.Collections;
using UnityEngine;

public class PetMovement : MonoBehaviour, IMoveProvider
{
    private float _goToTargetDelay = 2;
    private readonly float _stopDistance = 2;
    private Transform _followTarget;
    private Coroutine _activeMove;

    private bool _hasTarget;
    private Vector3 _targetPosition;

    public bool IsMoving => GetMovingState();//_activeMove != null || (FollowPosition - transform.position).magnitude > 0.1f;
    private Vector3 FollowPosition => _followTarget.position + new Vector3(2, 0, -1);

    public event Action<Vector3> PointToMoveSet;
    public event Action Reached;

    public void Init(Transform followTarget)
    {
        _followTarget = followTarget;
    }

    private void Update()
    {
        if (_hasTarget == true || _activeMove != null)
        {
            RotateTo(_targetPosition);
            return;
        }

        if (_followTarget == null)
        {
            return;
        }

        float deltaPosition = 5;
        transform.position = Vector3.MoveTowards(transform.position, FollowPosition, deltaPosition * Time.deltaTime);
        RotateTo(FollowPosition);
    }

    private bool GetMovingState()
    {
        if (_activeMove != null)
        {
            return true;
        }
        else if (_hasTarget == true)
        {
            return false;
        }
        else
        {
            return (FollowPosition - transform.position).magnitude > 0.1f;
        }
    }

    public void SetParameters(float goToTargetDelay)
    {
        _goToTargetDelay = goToTargetDelay;
    }

    public void GoTo(Vector3 position)
    {
        _hasTarget = true;
        _targetPosition = position;
        StopMovementCoroutine();

        _activeMove = StartCoroutine(MoveToPosition(position));
    }

    public void RemoveTarget()
    {
        _hasTarget = false;
        StopMovementCoroutine();
    }

    private IEnumerator MoveToPosition(Vector3 position)
    {
        float timeLeft = 0;
        float epsilon = 0.1f;
        Vector3 stopPosition = position + (transform.position - position).normalized * _stopDistance;

        PointToMoveSet?.Invoke(stopPosition);

        while (Vector3.Distance(transform.position, stopPosition) > epsilon)
        {
            float positionLerpSpot;

            if(_goToTargetDelay <= 0)
            {
                positionLerpSpot = 1;
            }
            else
            {
                positionLerpSpot = timeLeft / _goToTargetDelay;
            }

            transform.position = Vector3.Lerp(transform.position, stopPosition, positionLerpSpot);
            RotateTo(stopPosition);

            timeLeft += Time.deltaTime;
            yield return null;
        }

        _activeMove = null;
        Reached?.Invoke();
    }

    private void StopMovementCoroutine()
    {
        if (_activeMove != null)
        {
            StopCoroutine(_activeMove);
            _activeMove = null;
        }
    }

    private void RotateTo(Vector3 position)
    {
        Vector3 lookDirection = position - transform.position;

        if (lookDirection == Vector3.zero)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);

        float lerp = 8;
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, lerp * Time.deltaTime);
    }
}