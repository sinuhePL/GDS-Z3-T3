using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public abstract class Attack : ScriptableObject
    {
        public GameObject _weaponPrefab;
        public float _attackRange;
        public float _attackDelay;
        public LayerMask _targetMask;
        protected Transform _attackPoint;
        protected GameObject _myParent;
        public abstract void Initialize(Transform attackTransform, GameObject myParent);
        public abstract void MakeAttack(float attackDuration, System.Action attackCallback);
    }
}
