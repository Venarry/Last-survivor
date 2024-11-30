using Health;
using ObjectPool;
using System;
using UnityEngine;

namespace Targets
{
    [RequireComponent(typeof(HealthView))]
    public class Target : MonoBehaviour, IPoolObject<Target>
    {
        [SerializeField] private bool _isFriendly = false;
        private HealthView _healthView;
        private HealthModel _healthModel;

        public event Action<Target> LifeCycleEnded;

        public TargetType TargetType { get; private set; }
        public Vector3 Position => transform.position;
        public bool IsFriendly => _isFriendly;

        private void Awake()
        {
            _healthView = GetComponent<HealthView>();

            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        public void Init(TargetType targetType, HealthModel healthModel)
        {
            TargetType = targetType;
            _healthModel = healthModel;
            _healthView.Init(healthModel);

            _healthModel.HealthOver += PlaceInPool;
        }

        private void OnDestroy()
        {
            _healthModel.HealthOver -= PlaceInPool;
        }

        public void TakeDamage(float damage)
        {
            _healthModel.TakeDamage(damage);
        }

        public void PlaceInPool()
        {
            LifeCycleEnded?.Invoke(this);
        }

        public void Respawn(Vector3 spawnPosition, Quaternion rotation)
        {
            transform.position = spawnPosition;
            transform.rotation = rotation;
        }

        public void ResetSettings(float health)
        {
            _healthModel.SetMaxHealth(health);
            _healthModel.Restore();
        }
    }
}