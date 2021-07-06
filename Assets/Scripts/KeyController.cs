using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GDS3
{
    public class KeyController : Interactable
    {
        public override void Interact(Transform parentTransform, Vector3 targetPosition, float targetScale, float movementTime)
        {
            Color highlightColor = _highlightSpriteRenderer.color;
            highlightColor.a = 0;
            _highlightSpriteRenderer.color = highlightColor;
            transform.SetParent(parentTransform);
            transform.DOMove(targetPosition, movementTime);
            if (parentTransform.localScale.x < 0.0f)
            {
                transform.DOScaleX(-targetScale, movementTime);
            }
            else
            {
                transform.DOScaleX(targetScale, movementTime);
            }
            transform.DOScaleY(targetScale, movementTime);
        }
    }
}
