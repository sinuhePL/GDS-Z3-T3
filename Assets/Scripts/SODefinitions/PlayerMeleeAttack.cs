using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "PlayerMeleeAttack", menuName = "Scriptable Objects/Player Melee Attack")]
    public class PlayerMeleeAttack : Attack
    {
        public override void Initialize(Transform attackTransform, GameObject myParent)
        {
            base.Initialize(attackTransform, myParent);
            _attackPoint = attackTransform;
            _myParent = myParent;
        }

        public override void MakeAttack(float attackDuration, System.Action attackCallback)
        {
            IHitable myHit;
            Collider2D[] hitColliders;
            if (!_isGamePaused)
            {
                hitColliders = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _targetMask);
                if (hitColliders.Length > 0)
                {
                    foreach (Collider2D collider in hitColliders)
                    {
                        myHit = collider.gameObject.GetComponent<IHitable>();
                        if (myHit != null)
                        {
                            myHit.Hit();
                        }
                    }
                }
            }
        }
    }
}
