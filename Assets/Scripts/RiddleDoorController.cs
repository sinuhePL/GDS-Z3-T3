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
        private Vector3 _startingPosition;

        private void Awake()
        {
            _startingPosition = transform.position;
        }

        public void OpenDoor()
        {
            Sequence doorSequence = DOTween.Sequence();
            doorSequence.PrependInterval(_delayTime).OnComplete(() => _myAudioSource.Play());
            doorSequence.Append(transform.DOMove(_openPosition.position, _openingTime));
        }

        public void CloseDoor()
        {
            transform.DOMove(_startingPosition, _openingTime);
        }
    }
}
