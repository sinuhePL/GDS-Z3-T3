using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class PlayerDeathController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _myRigidbody;
        [SerializeField] private Vector3Reference _lastSpawnPoint;

        public void HandleDeath()
        {
            _myRigidbody.MovePosition(_lastSpawnPoint.Value);
        }
    }
}
