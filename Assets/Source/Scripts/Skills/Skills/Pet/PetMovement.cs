using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PetMovement : MonoBehaviour
{
    private Target _followTarget;
    private float _goToTargetDelay = 2;
    private float _stopDistance = 2;
    private Coroutine _activeMove;

    private bool _hasTarget;

    private Vector3 FollowPosition => _followTarget.Position + new Vector3(3, 0, -1);

    public void Init(Target followTarget)
    {
        _followTarget = followTarget;
    }

    private void Update()
    {
        if (_hasTarget == true || _activeMove != null)
            return;

        if (_followTarget == null)
            return;

        transform.position = FollowPosition;
    }

    public void SetParameters(float goToTargetDelay)
    {
        _goToTargetDelay = goToTargetDelay;
    }

    public void GoTo(Vector3 position)
    {
        _hasTarget = true;
        StopMovementCoroutine();

        _activeMove = StartCoroutine(MoveToPosition(position));
    }

    public void RemoveTarget()
    {
        _hasTarget = false;
        StopMovementCoroutine();

        _activeMove = StartCoroutine(MoveToPosition(FollowPosition));
    }

    private IEnumerator MoveToPosition(Vector3 position)
    {
        float timeLeft = 0;
        float epsilon = 0.1f;
        Vector3 stopPosition = position + (transform.position - position).normalized * _stopDistance;

        while (Vector3.Distance(transform.position, stopPosition) > epsilon)
        {
            float positionLerpSpot;

            if(_goToTargetDelay == 0)
            {
                positionLerpSpot = 1;
            }
            else
            {
                positionLerpSpot = timeLeft / _goToTargetDelay;
            }

            transform.position = Vector3.Lerp(transform.position, stopPosition, positionLerpSpot);

            timeLeft += Time.deltaTime;
            yield return null;
        }

        _activeMove = null;
    }

    private void StopMovementCoroutine()
    {
        if (_activeMove != null)
        {
            StopCoroutine(_activeMove);
            _activeMove = null;
        }
    }
}
