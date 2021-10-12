using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    [RequireComponent(typeof(Collider2D))]
    public class RespawnPointController : MonoBehaviour
    {
        [SerializeField] private Vector3Reference _lastRespawnPoint;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private ParticleSystem _respawnParticleSystem;
        [SerializeField] private AudioSource _myAudioSource;
        [SerializeField] private Sound _activationSound;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _activationSoundVolume;
        [SerializeField] private UnityEvent _respawnActivatedEvent;

        private bool _isActivated = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(_targetLayer == (_targetLayer | (1 << collision.gameObject.layer)) && !_isActivated)
            {
                _lastRespawnPoint.Value = collision.gameObject.transform.position;
                _respawnParticleSystem.Play();
                _activationSound.Play(_myAudioSource, _activationSoundVolume);
                _isActivated = true;
                _respawnActivatedEvent.Invoke();
            }
        }
    }
}
