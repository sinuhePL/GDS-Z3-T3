using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class AmbientSoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _myAudioSource;
        private bool _isGamePaused = false;
        private bool _wasSoundPlayed;

        private void Start()
        {
            _wasSoundPlayed = _myAudioSource.isPlaying;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                _myAudioSource.Play();
                _wasSoundPlayed = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                _myAudioSource.Pause();
                _wasSoundPlayed = false;
            }
        }

        public void PausePressed()
        {
            if(!_isGamePaused)
            {
                _myAudioSource.Stop();
            }
            else if(_wasSoundPlayed)
            {
                _myAudioSource.Play();
            }
            _isGamePaused = !_isGamePaused;
        }
    }
}
