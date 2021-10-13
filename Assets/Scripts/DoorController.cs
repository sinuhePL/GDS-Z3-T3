using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GDS3
{
    public class DoorController : Interactable
    {
        [SerializeField] private Transform _openPosition;
        [SerializeField] private Transform _matchingKey;
        [SerializeField] private HiddenController _myHiddenObject;
        [SerializeField] private AudioSource _myAudioSource;
        [SerializeField] private Sound _doorOpenSound;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _openDoorSoundVolume;
        [SerializeField] private float _delay;
        private Vector3 _closedPosition;
        private bool _isClosed;

        private new void Awake()
        {
            base.Awake();
            _closedPosition = transform.position;
            _isClosed = true;
        }

        private IEnumerator InteractCoroutine(PlayerCharacterController player)
        {
            yield return new WaitForSeconds(_delay);
            Color highlightColor = _highlightSpriteRenderer.color;
            highlightColor.a = 0;
            _highlightSpriteRenderer.color = highlightColor;
            _isClosed = false;
            if (player._pocket != null)
            {
                Destroy(player._pocket.gameObject);
                player._pocket = null;
            }
            _isActivationEnabled = false;
            _doorOpenSound.Play(_myAudioSource, _openDoorSoundVolume);
            if (_myHiddenObject != null)
            {
                _myHiddenObject.Hide(_interactionTime);
            }
            transform.DOMove(_openPosition.position, _interactionTime).OnComplete(() => _highlightSpriteRenderer.color = highlightColor);
        }

        public override void Interact(PlayerCharacterController player)
        {
            if (_isPlayerSmall.Value && (_matchingKey == null || player._pocket == _matchingKey) && _isClosed)
            {
                StartCoroutine(InteractCoroutine(player));
            }
        }
    }
}
