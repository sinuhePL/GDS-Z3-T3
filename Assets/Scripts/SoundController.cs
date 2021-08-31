using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class SoundController : MonoBehaviour
    {
        public Sound _stepSound;
        [Range(0.0f, 1.0f)] public float _stepVolume;
        public Sound _landSound;
        [Range(0.0f, 1.0f)] public float _landVolume;
        public Sound _jumpSound;
        [Range(0.0f, 1.0f)] public float _jumpVolume;
        public Sound _deathSound;
        [Range(0.0f, 1.0f)] public float _deathVolume;
        public Sound _attackSound;
        [Range(0.0f, 1.0f)] public float _attackVolume;
        public Sound _dashSound;
        [Range(0.0f, 1.0f)] public float _dashVolume;
        private AudioSource _myAudioSource;

        private void Awake()
        {
            _myAudioSource = gameObject.AddComponent<AudioSource>();
        }

        public void PlayStepSound()
        {
            _stepSound.Play(_myAudioSource, _stepVolume);
        }

        public void PlayLandSound()
        {
            _landSound.Play(_myAudioSource, _landVolume);
        }

        public void PlayJumpSound()
        {
            _jumpSound.Play(_myAudioSource, _jumpVolume);
        }

        public void PlayDeathSound()
        {
            _deathSound.Play(_myAudioSource, _deathVolume);
        }

        public void PlayAttackSound()
        {
            _attackSound.Play(_myAudioSource, _attackVolume);
        }

        public void PlayDashSound()
        {
            _dashSound.Play(_myAudioSource, _dashVolume);
        }
    }
}
