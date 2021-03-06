using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GDS3
{
    public class RiddleDoorController : MonoBehaviour
    {
        [SerializeField] private Transform _openPosition;
        [SerializeField] private float _openingTime;
        [SerializeField] private float _delayTime;
        [SerializeField] private AudioSource _myAudioSource;
        [SerializeField] private Sound _doorOpenSound;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _openDoorSoundVolume;
        private Vector3 _startingPosition;

        private IEnumerator PlaySoundWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _doorOpenSound.Play(_myAudioSource, _openDoorSoundVolume);
        }

        private void Awake()
        {
            _startingPosition = transform.position;
        }

        public void OpenDoor()
        {
            Sequence doorSequence = DOTween.Sequence();
            StartCoroutine(PlaySoundWithDelay(_delayTime));
            doorSequence.PrependInterval(_delayTime);
            doorSequence.Append(transform.DOMove(_openPosition.position, _openingTime));
        }

        public void CloseDoor()
        {
            transform.DOMove(_startingPosition, _openingTime);
        }
    }
}
