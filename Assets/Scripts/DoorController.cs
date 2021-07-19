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
        private Vector3 _closedPosition;
        private bool _isClosed;

        private new void Awake()
        {
            base.Awake();
            _closedPosition = transform.position;
            _isClosed = true;
        }

        public override void Interact(PlayerCharacterController player)
        {
            if (_isPlayerSmall.Value && (_matchingKey == null || player._pocket == _matchingKey) && _isClosed)
            {
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
                transform.DOMove(_openPosition.position, _interactionTime).OnComplete(() => _highlightSpriteRenderer.color = highlightColor);
            }
        }
    }
}
