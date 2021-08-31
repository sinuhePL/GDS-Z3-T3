using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class PlayerDeathController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _myRigidbody;
        [SerializeField] private Animator _myAnimator;
        [SerializeField] private Vector3Reference _lastSpawnPoint;
        [SerializeField] private BoolReference _isInputBlocked;
        public UnityEvent _killedEvent;

        public void HandleDeath()
        {
            _killedEvent.Invoke();
            _myRigidbody.isKinematic = true;
            _myRigidbody.velocity = Vector3.zero;
            _myRigidbody.transform.position = _lastSpawnPoint.Value;
            _myRigidbody.isKinematic = false;
        }
    }
}
