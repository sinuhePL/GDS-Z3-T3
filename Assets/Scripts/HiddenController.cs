using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GDS3
{
    public class HiddenController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _mySpriteRenderer;

        public void Hide(float hideTime)
        {
            _mySpriteRenderer.DOColor(new Color(_mySpriteRenderer.color.r, _mySpriteRenderer.color.g, _mySpriteRenderer.color.b, 0.0f), hideTime);
        }
    }
}
