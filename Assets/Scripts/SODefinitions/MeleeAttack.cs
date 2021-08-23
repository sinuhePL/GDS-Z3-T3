using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    [CreateAssetMenu(fileName = "MeleeAttack", menuName = "Scriptable Objects/Melee Attack")]
    public class MeleeAttack : Attack
    {
        private IEnumerator PerformAttack(float duration, System.Action attackCallback)
        {
            float elapsedTime = 0.0f;
            float timeStamp = Time.time;
            IHitable myHit;
            Collider2D[] hitColliders;
            bool isHit = false;
            yield return new WaitForSeconds(_attackDelay);
            while (elapsedTime < duration - _attackDelay && !isHit)
            {
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
                                isHit = true;
                            }
                        }
                    }
                    elapsedTime += Time.time - timeStamp;
                    timeStamp = Time.time;
                }
                yield return null;
            }
            attackCallback();
        }

        public override void Initialize(Transform attackTransform, GameObject myParent)
        {
            base.Initialize(attackTransform, myParent);
            _attackPoint = attackTransform;
            _myParent = myParent;
        }

        public override void MakeAttack(float attackDuration, System.Action attackCallback)
        {
            MonoBehaviour parentMonoBehaviour = _myParent.GetComponent<MonoBehaviour>();
            if (parentMonoBehaviour != null)
            {
                Debug.Log("zaczynam atak");
                parentMonoBehaviour.StartCoroutine(PerformAttack(attackDuration, attackCallback));
            }
        }
    }
}
