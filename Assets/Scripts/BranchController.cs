using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class BranchController : MonoBehaviour
    {
        public BoolReference _isPlayerSmall;
        public Animator _myAnimator;
        public BoxCollider2D _myCollider;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                BendBranch();
            }
        }

        public void BendBranch()
        {
            if (!_isPlayerSmall.Value)
            {
                _myAnimator.SetTrigger("down");
                _myCollider.size = new Vector2(3.6f, 3.0f);
                _myCollider.offset = new Vector2(1.6f, 1.5f);
            }
        }
    }
}
