using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [RequireComponent(typeof(Collider2D))]
    public class RespawnPointController : MonoBehaviour
    {
        [SerializeField] private Vector3Reference _lastRespawnPoint;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private ParticleSystem _respawnParticleSystem;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(_targetLayer == (_targetLayer | (1 << collision.gameObject.layer)))
            {
                _lastRespawnPoint.Value = collision.gameObject.transform.position;
                _respawnParticleSystem.Play();
            }
        }
    }
}
