using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GDS3
{
    public class PlayerSoundController : SoundController
    {
        [SerializeField] private Sound _landSound;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _landVolume;
        [SerializeField] private Sound _dashSound;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _dashVolume;
        [SerializeField] private Sound _fallSound;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _fallVolume;
        [SerializeField] private Sound _sacrificeSound;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _sacrificeVolume;

        public void PlayLandSound()
        {
            Assert.IsNotNull(_landSound);
            _landSound.Play(_myAudioSource, _landVolume);
        }

        public void PlayDashSound()
        {
            Assert.IsNotNull(_dashSound);
            _dashSound.Play(_myAudioSource, _dashVolume);
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
