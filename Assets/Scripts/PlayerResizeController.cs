using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class PlayerResizeController : MonoBehaviour, IResizable
    {
        [SerializeField] private BoolReference _isSmallSize;
        [SerializeField] private FloatReference _myMaxVelocity;
        public UnityEvent _changeSizeEvent;

        private void Awake()
        {
            _isSmallSize.Value = false;
        }

        public IEnumerator Resize(float resizeFactor, float resizeTime)
        {
            bool isFacingRight;
            float currentScaleX, currentScaleY, interpolationPoint;

            _isSmallSize.Value = !_isSmallSize.Value;
            _changeSizeEvent.Invoke();
            if (transform.localScale.x > 0.0)
            {
                isFacingRight = true;
            }
            else
            {
                isFacingRight = false;
            }
            float startingScaleX = transform.localScale.x;
            float targetScaleX = transform.localScale.x * resizeFactor;
            float startingScaleY = transform.localScale.y;
            float targetScaleY = transform.localScale.y * resizeFactor;
            float startingMaxVelocity = _myMaxVelocity.Value;
            float targetMaxVelocity = _myMaxVelocity.Value * resizeFactor;
            for (float t = 0; t < resizeTime; t += Time.deltaTime)
            {
                interpolationPoint = t / resizeTime;
                interpolationPoint = interpolationPoint * interpolationPoint * (3f - 2f * interpolationPoint);
                if (isFacingRight && transform.localScale.x < 0.0 || !isFacingRight && transform.localScale.x > 0.0) // used when character orientation flipped during shrinking
                {
                    isFacingRight = !isFacingRight;
                    targetScaleX = -targetScaleX;
                    startingScaleX = -startingScaleX;
                }
                currentScaleX = Mathf.Lerp(startingScaleX, targetScaleX, interpolationPoint);
                currentScaleY = Mathf.Lerp(startingScaleY, targetScaleY, interpolationPoint);
                transform.localScale = new Vector3(currentScaleX, currentScaleY, 0.0f);
                _myMaxVelocity.Value = Mathf.Lerp(startingMaxVelocity, targetMaxVelocity, interpolationPoint);
                yield return 0;
            }
            transform.localScale = new Vector3(targetScaleX, targetScaleY, 0.0f);
            _myMaxVelocity.Value = targetMaxVelocity;
        }

        public bool CheckIfSmall()
        {
            return _isSmallSize.Value;
        }
    }
}
