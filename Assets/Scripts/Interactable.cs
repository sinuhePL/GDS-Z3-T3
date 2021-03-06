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
        [SerializeField] protected BoolReference _isPlayerSmall;
        [SerializeField] protected FloatReference _sizeChangeFactor;
        [SerializeField] protected float _highlightTransitionTime;
        [SerializeField] protected FloatReference _detectionDistance;
        [SerializeField] protected float _interactionTime;
        [SerializeField] protected bool _interactWhenSmall;
        protected bool _isActivatorNearby;
        protected bool _isActivationEnabled;
        protected bool _stopHighlight;

        protected virtual void Awake()
        {
            _isActivatorNearby = false;
            Color highlightColor = _highlightSpriteRenderer.color;
            highlightColor.a = 0;
            _highlightSpriteRenderer.color = highlightColor;
            _isActivationEnabled = true;
            _stopHighlight = false;
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
            for (float t = 0; t < transitionTime && _isActivatorNearby == wasActivatorNearby && !_stopHighlight; t += Time.deltaTime)
            {
                float interpolationPoint = t / transitionTime;
                highlightColor = _highlightSpriteRenderer.color;
                highlightColor.a = Mathf.Lerp(startingAlpha, targetAlpha, interpolationPoint);
                _highlightSpriteRenderer.color = highlightColor;
                yield return 0;
            }
            if (_stopHighlight)
            {
                highlightColor = _highlightSpriteRenderer.color;
                highlightColor.a = 0.0f;
                _highlightSpriteRenderer.color = highlightColor;
            }
        }

        protected void Update()
        {
            if (_isActivationEnabled)
            {
                float detectionDistance = _detectionDistance.Value;
                if(_isPlayerSmall.Value)
                {
                    detectionDistance = _detectionDistance.Value / _sizeChangeFactor.Value;
                }
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionDistance, _activatingLayerMask);
                if (colliders.Length > 0)
                {
                    if (!_isActivatorNearby && (_isPlayerSmall.Value && _interactWhenSmall || !_isPlayerSmall.Value && !_interactWhenSmall))
                    {
                        _isActivatorNearby = true;
                        StartCoroutine(ChangeHighlight());
                    }
                }
                else
                {
                    if (_isActivatorNearby && (_isPlayerSmall.Value && _interactWhenSmall || !_isPlayerSmall.Value && !_interactWhenSmall))
                    {
                        _isActivatorNearby = false;
                        StartCoroutine(ChangeHighlight());
                    }
                }
            }
        }

        public abstract void Interact(PlayerCharacterController player);
    }
}
