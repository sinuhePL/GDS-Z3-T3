using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class EnemyDeathController : MonoBehaviour
    {
        [SerializeField] private Animator _myAnimator;

        public void HandleDeath()
        {
            _myAnimator.SetTrigger("die");
        }

        public void Disappear()
        {
            gameObject.SetActive(false);
        }
    }
}
