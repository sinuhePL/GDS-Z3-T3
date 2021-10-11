using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GDS3
{
    public class PlayerSoundController : SoundController
    {
        public Sound _landSound;
        [Range(0.0f, 1.0f)] public float _landVolume;
        public Sound _dashSound;
        [Range(0.0f, 1.0f)] public float _dashVolume;
        public Sound _fallSound;
        [Range(0.0f, 1.0f)] public float _fallVolume;
        public Sound _sacrificeSound;
        [Range(0.0f, 1.0f)] public float _sacrificeVolume;
        public Sound _shrinkSound;
        [Range(0.0f, 1.0f)] public float _shrinkVolume;
        public Sound _enlargeSound;
        [Range(0.0f, 1.0f)] public float _enlargeVolume;

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

        public void PlayShrinkSound()
        {
            Assert.IsNotNull(_shrinkSound);
            _shrinkSound.Play(_myAudioSource, _shrinkVolume);
        }

        public void PlayEnlargeSound()
        {
            Assert.IsNotNull(_enlargeSound);
            _enlargeSound.Play(_myAudioSource, _enlargeVolume);
        }
    }
}
