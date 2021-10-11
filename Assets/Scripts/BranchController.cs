using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class BranchController : MonoBehaviour
    {
        public BoolReference _isPlayerSmall;
        public Animator _myAnimator;
        public EdgeCollider2D _myCollider;
        public EdgeCollider2D _myBottomCollider;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" && !_isPlayerSmall.Value)
            {
                if (collision.otherCollider == (_myCollider as Collider2D))
                {
                    _myAnimator.SetTrigger("down");
                }
            }
        }

        public void DeactivateCollider()
        {
            _myCollider.enabled = false;
            _myBottomCollider.enabled = false;
            _myAnimator.ResetTrigger("down");
        }

        public void ActivateCollider()
        {
            _myCollider.enabled = true;
            _myBottomCollider.enabled = true;
        }
    }
}
