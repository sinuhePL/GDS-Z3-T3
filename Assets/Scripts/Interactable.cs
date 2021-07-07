using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer _mySpriteRenderer;
        [SerializeField] protected SpriteRenderer _highlightSpriteRenderer;
        [SerializeField] protected LayerMask _activatingLayerMask;
        [SerializeField] protected float _highlightTransitionTime;
        [SerializeField] protected float _detectionDistance;
        protected bool _isActivatorNearby;

        protected void Awake()
        {
            _isActivatorNearby = false;
            Color highlightColor = _highlightSpriteRenderer.color;
            highlightColor.a = 0;
            _highlightSpriteRenderer.color = highlightColor;
        }

        protected IEnumerator ChangeHighlight()
        {
            Color highlightColor;
            float targetAlpha;
            float startingAlpha = _highlightSpriteRenderer.color.a;
            bool wasActivatorNearby = _isActivatorNearby;
            if (_isActivatorNearby)
            {
                targetAlpha = 1.0f;
            }
            else
            {
                targetAlpha = 0.0f;
            }
            float transitionTime = _highlightTransitionTime * Mathf.Abs(targetAlpha - startingAlpha);
            for (float t = 0; t < transitionTime && _isActivatorNearby == wasActivatorNearby; t += Time.deltaTime)
            {
                float interpolationPoint = t / transitionTime;
                highlightColor = _highlightSpriteRenderer.color;
                highlightColor.a = Mathf.Lerp(startingAlpha, targetAlpha, interpolationPoint);
                _highlightSpriteRenderer.color = highlightColor;
                yield return 0;
            }
        }

        private void Update()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _detectionDistance, _activatingLayerMask);
            if(colliders.Length > 0)
            {
                if (!_isActivatorNearby)
                {
                    _isActivatorNearby = true;
                    StartCoroutine(ChangeHighlight());
                }
            }
            else
            {
                if(_isActivatorNearby)
                {
                    _isActivatorNearby = false;
                    StartCoroutine(ChangeHighlight());
                }
            }
        }

        public abstract void Interact(Transform parentTransform, float targetScale, float movementTime);
    }
}
