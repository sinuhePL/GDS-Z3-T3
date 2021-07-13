using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GDS3
{
    public class DoorController : Interactable
    {
        [SerializeField] private Transform _openPosition;
        private Vector3 _closedPosition;

        private new void Awake()
        {
            base.Awake();
            _closedPosition = transform.position;
        }

        public override void Interact(Transform parentTransform)
        {
            if (_isPlayerSmall.Value)
            {

                Color highlightColor = _highlightSpriteRenderer.color;
                highlightColor.a = 0;
                _highlightSpriteRenderer.color = highlightColor;
                transform.DOMove(_openPosition.position, _interactionTime).OnComplete(() => _highlightSpriteRenderer.color = highlightColor);
            }
        }
    }
}
