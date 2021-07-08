using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public abstract class Attack : ScriptableObject, IListenable
    {
        public GameObject _weaponPrefab;
        public float _attackRange;
        public float _attackDelay;
        public LayerMask _targetMask;
        public GameEvent _pauseEvent;
        protected Transform _attackPoint;
        protected GameObject _myParent;
        protected bool _isGamePaused;

        protected void OnDisable()
        {
            _pauseEvent.UnregisterListener(this);
        }

        public void OnEventRaised(GameEvent gameEvent)
        {
            if (gameEvent == _pauseEvent)
            {
                _isGamePaused = !_isGamePaused;
            }
        }

        public virtual void Initialize(Transform attackTransform, GameObject myParent)
        {
            _isGamePaused = false;
            _pauseEvent.RegisterListener(this);
        }

        public abstract void MakeAttack(float attackDuration, System.Action attackCallback);
    }
}
