using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GDS3
{
    public class KeyController : Interactable
    {
        public override void Interact(Transform parentTransform, float targetScale, float movementTime)
        {
            Color highlightColor = _highlightSpriteRenderer.color;
            highlightColor.a = 0;
            _highlightSpriteRenderer.color = highlightColor;
            transform.SetParent(parentTransform);
            transform.DOMove(parentTransform.position, movementTime);
            transform.DOScale(targetScale, movementTime);
            _mySpriteRenderer.sortingOrder = 2;
        }
    }
}
