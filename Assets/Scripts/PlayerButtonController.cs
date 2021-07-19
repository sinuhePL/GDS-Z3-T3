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

        public override void Interact(PlayerCharacterController player)
        {
            if (!_isPressed && Vector3.Distance(transform.position, player._groundCheck.position) < 0.06f)
            {
                Color highlightColor = _highlightSpriteRenderer.color;
                highlightColor.a = 0;
                _highlightSpriteRenderer.color = highlightColor;
                _isPressed = true;
                _isActivationEnabled = false;
                transform.DOMove(_pressedPosition.position, _interactionTime);
                _riddleController.ButtonPressed(this);
            }
        }

        public void ReleaseButton()
        {
            transform.DOMove(_startingPosition, _interactionTime).OnComplete(()=>ResetButton());
        }
    }
}
