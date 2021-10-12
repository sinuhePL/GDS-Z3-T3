using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GDS3
{
    public class PlayerButtonController : Interactable
    {
        [SerializeField] private Riddle1Controller _riddleController;
        [SerializeField] private Transform _pressedPosition;
        [SerializeField] private AudioSource _myAudioSource;
        [SerializeField] private Sound _buttonPressedSound;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _buttonPressedSoundVolume;
        private Vector3 _startingPosition;
        private bool _isPressed;

        private new void Awake()
        {
            base.Awake();
            _startingPosition = transform.position;
            _isPressed = false;
        }

        private void ResetButton()
        {
            _isPressed = false;
            _isActivationEnabled = true;
        }

        private IEnumerator PlaySoundWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _buttonPressedSound.Play(_myAudioSource, _buttonPressedSoundVolume);
        }

        public override void Interact(PlayerCharacterController player)
        {
            Debug.Log("Interact distance: " + Vector3.Distance(transform.position, player._groundCheck.position));
            if (!_isPressed && Vector3.Distance(transform.position, player._groundCheck.position) < player._interactionRange.Value)
            {
                Color highlightColor = _highlightSpriteRenderer.color;
                highlightColor.a = 0;
                _highlightSpriteRenderer.color = highlightColor;
                _isPressed = true;
                _isActivationEnabled = false;
                transform.DOMove(_pressedPosition.position, _interactionTime).OnComplete(() => _highlightSpriteRenderer.color = highlightColor);
                _buttonPressedSound.Play(_myAudioSource, _buttonPressedSoundVolume);
                _riddleController.ButtonPressed(this);
            }
        }

        public void ReleaseButton()
        {
            Sequence buttonSequence = DOTween.Sequence();
            StartCoroutine(PlaySoundWithDelay(_interactionTime * 2));
            buttonSequence.PrependInterval(_interactionTime*2);
            buttonSequence.Append(transform.DOMove(_startingPosition, _interactionTime).OnComplete(()=>ResetButton()));
        }
    }
}
