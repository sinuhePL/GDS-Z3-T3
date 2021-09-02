using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class AmbientSoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _myAudioSource;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                _myAudioSource.Play();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                _myAudioSource.Pause();
            }
        }
    }
}
