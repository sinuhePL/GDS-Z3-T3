using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GDS3
{
    public class KeyController : Interactable
    {
        [SerializeField] private BoolReference _isPlayerSmall;
        [SerializeField] private BoolReference _isInputBlocked;
        private bool _isTaken;

        private new void Awake()
        {
            base.Awake();
            _isTaken = false;
        }

        public override void Interact(Transform parentTransform, float targetScale, float movementTime)
        {
            if (!_isTaken && !_isPlayerSmall.Value)
            {
                Color highlightColor = _highlightSpriteRenderer.color;
                highlightColor.a = 0;
                _highlightSpriteRenderer.color = highlightColor;
                _isInputBlocked.Value = true;
                transform.SetParent(parentTransform);
                transform.DOMove(parentTransform.position, movementTime);
                transform.DOScale(targetScale, movementTime).OnComplete(() => { _isInputBlocked.Value = false; _highlightSpriteRenderer.color = highlightColor; });
                _mySpriteRenderer.sortingOrder = 2;
                _isTaken = true;
            }
        }
    }
}
