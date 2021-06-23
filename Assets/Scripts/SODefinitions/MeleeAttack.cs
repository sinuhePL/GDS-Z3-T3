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

        public override bool MakeAttack()
        {
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _targetMask);
            if (hitPlayers.Length > 0)
            {
                _targetHitEvent.Invoke();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
