using System.Collections;
using UnityEngine;

public abstract class DamageIndicator : MonoBehaviour
{
    [SerializeField] private Transform _shakeTarget;
    private float _defaultScale;

    protected virtual float ShakeStrength { get; } = 1.0f;
    protected virtual float ScaleFactor { get; } = 1.2f;
    protected virtual float Duration { get; } = 0.3f;

    private void Awake()
    {
        if(_shakeTarget != null)
        {
            _defaultScale = _shakeTarget.localScale.x;
        }
    }

    public abstract void Shake();

    protected void ShakeSize() => StartCoroutine(ProcessSize());

    protected void ShakeRotation() => StartCoroutine(ProcessRotation());

    private IEnumerator ProcessSize()
    {
        float timeLeft = 0;
        float middleTimeSpot = Duration / 2;

        while (timeLeft < Duration)
        {
            float lerpSpot = timeLeft / middleTimeSpot;

            if(lerpSpot > 1)
            {
                lerpSpot = 2 - lerpSpot;
            }

            float scale = Mathf.Lerp(_defaultScale, _defaultScale * ScaleFactor, lerpSpot);
            _shakeTarget.localScale = new(scale, scale, scale);
            timeLeft += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator ProcessRotation()
    {
        float tiltAngle = 15f;
        Vector3 targetTilt = new(Random.Range(-tiltAngle, tiltAngle), Random.Range(-tiltAngle, tiltAngle), Random.Range(-tiltAngle, tiltAngle));
        float timeLeft = 0;
        float tiltpartDuration = Duration / 3;

        while (timeLeft < Duration)
        {
            float lerpSpot = timeLeft / tiltpartDuration;

            switch (lerpSpot)
            {
                case > 2:
                    lerpSpot = lerpSpot - 2;
                    transform.rotation = Quaternion.Lerp(_shakeTarget.rotation, Quaternion.identity, lerpSpot);
                    break;

                case > 1:
                    lerpSpot = lerpSpot - 1;
                    transform.rotation = Quaternion.Lerp(_shakeTarget.rotation, Quaternion.Euler(-targetTilt * 0.5f), lerpSpot);
                    break;

                case > 0:
                    transform.rotation = Quaternion.Lerp(_shakeTarget.rotation, Quaternion.Euler(targetTilt), lerpSpot);
                    break;
            }
            Debug.Log(lerpSpot);
            timeLeft += Time.deltaTime;

            yield return null;
        }
    }
}