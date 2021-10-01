using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GDS3
{
    public class SoundController : MonoBehaviour
    {
        public AudioSource _myAudioSource;
        public Sound _stepSound;
        [Range(0.0f, 1.0f)] public float _stepVolume;
        public Sound _landSound;
        [Range(0.0f, 1.0f)] public float _landVolume;
        public Sound _jumpSound;
        [Range(0.0f, 1.0f)] public float _jumpVolume;
        public Sound _hitSound;
        [Range(0.0f, 1.0f)] public float _hitVolume;
        public Sound _deathSound;
        [Range(0.0f, 1.0f)] public float _deathVolume;
        public Sound _attackSound;
        [Range(0.0f, 1.0f)] public float _attackVolume;
        public Sound _dashSound;
        [Range(0.0f, 1.0f)] public float _dashVolume;
        public Sound _cooldownSound;
        [Range(0.0f, 1.0f)] public float _cooldownVolume;
        public Sound _teleportSound;
        [Range(0.0f, 1.0f)] public float _teleportVolume;
        public Sound _fallSound;
        [Range(0.0f, 1.0f)] public float _fallVolume;
        public Sound _sacrificeSound;
        [Range(0.0f, 1.0f)] public float _sacrificeVolume;

        public void PlayStepSound()
        {
            Assert.IsNotNull(_stepSound);
            _stepSound.Play(_myAudioSource, _stepVolume);
        }

        public void PlayLandSound()
        {
            Assert.IsNotNull(_landSound);
            _landSound.Play(_myAudioSource, _landVolume);
        }

        public void PlayJumpSound()
        {
            Assert.IsNotNull(_jumpSound);
            _jumpSound.Play(_myAudioSource, _jumpVolume);
        }

        public void PlayHitSound()
        {
            Assert.IsNotNull(_hitSound);
            _hitSound.Play(_myAudioSource, _hitVolume);
        }

        public void PlayDeathSound()
        {
            Assert.IsNotNull(_deathSound);
            _deathSound.Play(_myAudioSource, _deathVolume);
        }

        public void PlayAttackSound()
        {
            Assert.IsNotNull(_attackSound);
            _attackSound.Play(_myAudioSource, _attackVolume);
        }

        public void PlayDashSound()
        {
            Assert.IsNotNull(_dashSound);
            _dashSound.Play(_myAudioSource, _dashVolume);
        }

        public void PlayCooldownSound()
        {
            Assert.IsNotNull(_cooldownSound);
            _cooldownSound.Play(_myAudioSource, _cooldownVolume);
        }

        public void PlayTeleportSound()
        {
            Assert.IsNotNull(_teleportSound);
            _teleportSound.Play(_myAudioSource, _teleportVolume);
        }

        public void PlayFallSound()
        {
            Assert.IsNotNull(_fallSound);
            _fallSound.Play(_myAudioSource, _fallVolume);
        }

        public void PlaySacrificeSound()
        {
            Assert.IsNotNull(_sacrificeSound);
            _sacrificeSound.Play(_myAudioSource, _sacrificeVolume);
        }
    }
}
