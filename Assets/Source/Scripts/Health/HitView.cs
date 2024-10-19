using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitView : MonoBehaviour
{
    [SerializeField] private Transform _shakeTarget;
    [SerializeField] private List<AudioClip> _hitSounds;
    [SerializeField] private ParticleSystem _destroyParticle;

    private AudioSource _audioSource;
    private HealthModel _healthModel;
    private float _defaultScale;
    private Quaternion _defaultRotation;

    protected virtual float ShakeStrength { get; } = 1.0f;
    protected virtual float ScaleFactor { get; } = 1.2f;
    protected virtual float Duration { get; } = 0.3f;

    private void Awake()
    {
        if(_shakeTarget != null)
        {
            _defaultScale = _shakeTarget.localScale.x;
            _defaultRotation = _shakeTarget.localRotation;
        }
    }

    public void Init(HealthModel healthModel, AudioSource audioSource)
    {
        _healthModel = healthModel;
        _audioSource = audioSource;
        _healthModel.DamageReceived += Shake;
        _healthModel.HealthOver += OnHealthOver;
    }

    private void OnDestroy()
    {
        _healthModel.DamageReceived -= Shake;
        _healthModel.HealthOver -= OnHealthOver;
    }

    public abstract void Shake();

    protected void ShakeSize()
    {
        if (gameObject.activeInHierarchy == false)
            return;

        StartCoroutine(ProcessSize());
    }

    protected void ShakeRotation() 
    {
        if (gameObject.activeInHierarchy == false)
            return;

        StartCoroutine(ProcessRotation());
    }

    protected void ActivateSound()
    {
        int soundIndex = Random.Range(0, _hitSounds.Count);
        AudioClip audioClip = _hitSounds[soundIndex];

        float volumeScale = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(audioClip, volumeScale);
    }

    private void OnHealthOver()
    {
        Vector3 particleOffset = new(0f, 0.2f, 0f);
        ParticleSystem destroyParticle = Instantiate(_destroyParticle, transform.position + particleOffset, _destroyParticle.transform.localRotation);
        destroyParticle.Play();
    }

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
        Vector3 tilt = new(Random.Range(-tiltAngle, tiltAngle), Random.Range(-tiltAngle, tiltAngle), Random.Range(-tiltAngle, tiltAngle));
        float timeLeft = 0;
        float tiltPartDuration = Duration / 3;

        while (timeLeft < Duration)
        {
            float lerpSpot = timeLeft / tiltPartDuration;
            Quaternion targetTilt = _defaultRotation;

            switch (lerpSpot)
            {
                case > 2:
                    lerpSpot = lerpSpot - 2;
                    targetTilt = _defaultRotation;
                    break;

                case > 1:
                    lerpSpot = lerpSpot - 1;
                    targetTilt = Quaternion.Euler(-tilt * 0.5f);
                    break;

                case > 0:
                    targetTilt = Quaternion.Euler(tilt);
                    break;
            }

            _shakeTarget.localRotation = Quaternion.Lerp(_shakeTarget.localRotation, targetTilt, lerpSpot);
            timeLeft += Time.deltaTime;

            yield return null;
        }
    }
}