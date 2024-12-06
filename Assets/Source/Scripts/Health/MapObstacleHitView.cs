using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Health
{
    public class MapObstacleHitView : MonoBehaviour
    {
        private readonly float _scaleFactor = 1.2f;
        private readonly float _duration = 0.3f;

        [SerializeField] private Transform _shakeTarget;
        [SerializeField] private List<AudioClip> _hitSounds;
        [SerializeField] private ParticleSystem _destroyParticle;

        private AudioSource _audioSource;
        private HealthModel _healthModel;
        private float _defaultScale;
        private Quaternion _defaultRotation;

        private void Awake()
        {
            _defaultScale = _shakeTarget.localScale.x;
            _defaultRotation = _shakeTarget.localRotation;
        }

        private void OnDestroy()
        {
            _healthModel.DamageReceived -= Shake;
            _healthModel.HealthOver -= OnHealthOver;
        }

        public void Init(HealthModel healthModel, AudioSource audioSource)
        {
            _healthModel = healthModel;
            _audioSource = audioSource;
            _healthModel.DamageReceived += Shake;
            _healthModel.HealthOver += OnHealthOver;
        }

        public void Shake()
        {
            ShakeSize();
            ShakeRotation();
            ActivateSound();
        }

        protected void ShakeSize()
        {
            if (gameObject.activeInHierarchy == false)
            {
                return;
            }

            StartCoroutine(ChangeSize());
        }

        protected void ShakeRotation()
        {
            if (gameObject.activeInHierarchy == false)
            {
                return;
            }

            StartCoroutine(ChangeRotation());
        }

        protected void ActivateSound()
        {
            int soundIndex = Random.Range(0, _hitSounds.Count);
            AudioClip audioClip = _hitSounds[soundIndex];

            float minVolumeScale = 0.9f;
            float maxVolumeScale = 1.1f;
            float volumeScale = Random.Range(minVolumeScale, maxVolumeScale);

            _audioSource.PlayOneShot(audioClip, volumeScale);
        }

        private void OnHealthOver()
        {
            Vector3 particleSpawnOffset = new (0f, 0.2f, 0f);
            ParticleSystem destroyParticle = Instantiate(
                _destroyParticle, transform.position + particleSpawnOffset, _destroyParticle.transform.localRotation);
            destroyParticle.Play();
        }

        private IEnumerator ChangeSize()
        {
            float timeLeft = 0;
            float middleTimeSpot = _duration / 2;
            float valueToChangeDirection = 1;
            float maxDuretionMultiplier = 2;

            while (timeLeft < _duration)
            {
                float lerpSpot = timeLeft / middleTimeSpot;

                if (lerpSpot > valueToChangeDirection)
                {
                    lerpSpot = maxDuretionMultiplier - lerpSpot;
                }

                float scale = Mathf.Lerp(_defaultScale, _defaultScale * _scaleFactor, lerpSpot);
                _shakeTarget.localScale = new (scale, scale, scale);
                timeLeft += Time.deltaTime;

                yield return null;
            }
        }

        private IEnumerator ChangeRotation()
        {
            const int FirstStepValue = 2;
            const int SecondStepValue = 1;
            const int ThirdStepValue = 0;

            float tiltAngle = 15f;
            Vector3 tilt = new (
                Random.Range(-tiltAngle, tiltAngle),
                Random.Range(-tiltAngle, tiltAngle),
                Random.Range(-tiltAngle, tiltAngle));

            float timeLeft = 0;
            float tiltPartDuration = _duration / 3;

            while (timeLeft < _duration)
            {
                float lerpSpot = timeLeft / tiltPartDuration;
                Quaternion targetTilt = _defaultRotation;

                switch (lerpSpot)
                {
                    case > FirstStepValue:
                        lerpSpot -= FirstStepValue;
                        targetTilt = _defaultRotation;
                        break;

                    case > SecondStepValue:
                        lerpSpot -= SecondStepValue;
                        targetTilt = Quaternion.Euler(-tilt * 0.5f);
                        break;

                    case > ThirdStepValue:
                        targetTilt = Quaternion.Euler(tilt);
                        break;
                }

                _shakeTarget.localRotation = Quaternion.Lerp(_shakeTarget.localRotation, targetTilt, lerpSpot);
                timeLeft += Time.deltaTime;

                yield return null;
            }
        }
    }
}