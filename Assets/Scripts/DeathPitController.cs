using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class DeathPitController : MonoBehaviour
    {
        [SerializeField] private LayerMask _hitMask;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            IHitable myHit;
            bool isTarget = _hitMask == (_hitMask | (1 << collision.gameObject.layer));
            if (isTarget)
            {
                myHit = collision.gameObject.GetComponent<IHitable>();
                if (myHit != null)
                {
                    myHit.Hit();
                }
            }
        }
    }
}
