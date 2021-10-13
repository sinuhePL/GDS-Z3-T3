using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class SmallObjectController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _mySpriteRenderer;
        [SerializeField] private BoolReference _isSmallSize;
        [SerializeField] private FloatReference _sizeChangeTime;
        [SerializeField] private Collider2D _myCollider;
        private float _initialAlpha;
        private int _resizeCoroutineId;

        private void Awake()
        {
            _initialAlpha = _mySpriteRenderer.color.a;
            _mySpriteRenderer.color = new Color(_mySpriteRenderer.color.r, _mySpriteRenderer.color.g, _mySpriteRenderer.color.b, 0.0f);
            _resizeCoroutineId = 0;
            if (_myCollider != null)
            {
                _myCollider.enabled = false;
            }
        }

        private IEnumerator ChangeAlpha()
        {
            float currentAlpha, interpolationPoint;
            float startingAlpha;
            float targetAlpha;
            float proportion = _sizeChangeTime.Value/ _initialAlpha;
            int myId = Random.Range(1, 999999999);
            _resizeCoroutineId = myId;
            if (_isSmallSize.Value)
            {
                startingAlpha = _mySpriteRenderer.color.a;
                targetAlpha = _initialAlpha;
                if (_myCollider != null)
                {
                    _myCollider.enabled = true;
                }
            }
            else
            {
                startingAlpha = _mySpriteRenderer.color.a;
                targetAlpha = 0.0f;
                if (_myCollider != null)
                {
                    _myCollider.enabled = false;
                }
            }
            float resizeTime = proportion * Mathf.Abs(startingAlpha - targetAlpha);
            for (float t = 0; t < resizeTime && myId == _resizeCoroutineId; t += Time.deltaTime)
            {
                interpolationPoint = t / resizeTime;
                currentAlpha = Mathf.Lerp(startingAlpha, targetAlpha, interpolationPoint);
                _mySpriteRenderer.color = new Color(_mySpriteRenderer.color.r, _mySpriteRenderer.color.g, _mySpriteRenderer.color.b, currentAlpha);
                yield return 0;
            }
            if(myId == _resizeCoroutineId)
            {
                _mySpriteRenderer.color = new Color(_mySpriteRenderer.color.r, _mySpriteRenderer.color.g, _mySpriteRenderer.color.b, targetAlpha);
            }
        }

        public void ChangeOpacity()
        {
            StartCoroutine(ChangeAlpha());
        }
    }
}
