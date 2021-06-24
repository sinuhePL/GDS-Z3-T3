using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    [CreateAssetMenu(fileName = "MeleeAttack", menuName = "Scriptable Objects/Melee Attack")]
    public class MeleeAttack : Attack
    {
        public override void Initialize(Transform attackTransform, GameObject myParent)
        {
            _attackPoint = attackTransform;
            _myParent = myParent;
        }

        public override void MakeAttack(System.Action attackCallback)
        {
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _targetMask);
            if (hitPlayers.Length > 0)
            {
                _targetHitEvent.Invoke();
                attackCallback();
            }
        }
    }
}
