using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class CharacterResizeController : MonoBehaviour
    {
        [SerializeField] private BoolReference _isResizableSmall;
        [SerializeField] private LayerMask _deadlyForSmallMask;
        [SerializeField] private UnityEvent _killedEvent;
        private int _resizeCoroutineId;
        private Vector3 _bigScale;
        private Vector3 _smallScale;
        private IResizable _myResizable;

        private void Awake()
        {
            _isResizableSmall.Value = false;
            _myResizable = GetComponent<IResizable>();
            _resizeCoroutineId = 0;
            Random.InitState((int)System.DateTime.Now.Ticks);
            if (_myResizable != null)
            {
                if (_isResizableSmall.Value)
                {
                    _smallScale = transform.localScale;
                    _bigScale = transform.localScale * _myResizable.GetFactor();
                }
                else
                {
                    _smallScale = transform.localScale / _myResizable.GetFactor();
                    _bigScale = transform.localScale;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            bool isDeadly = _deadlyForSmallMask == (_deadlyForSmallMask | (1 << collision.gameObject.layer));
            if (isDeadly && _isResizableSmall.Value)
            {
                _killedEvent.Invoke();
            }
        }

        private IEnumerator Resize(float resizeFactor, float resizeTime)
        {
            bool isFacingRight;
            float currentScaleX, currentScaleY, interpolationPoint, currentSpeed, targetSpeed, startingScaleX, targetScaleX, startingScaleY, targetScaleY;

            int myId = Random.Range(1, 999999999);
            float proportion = resizeTime / (_bigScale.y - _smallScale.y);
            _resizeCoroutineId = myId;
            if (transform.localScale.x > 0.0)
            {
                isFacingRight = true;
                if(_bigScale.x < 0.0)
                {
                    _bigScale.x = -_bigScale.x;
                }
                if (_smallScale.x < 0.0)
                {
                    _smallScale.x = -_smallScale.x;
                }
            }
            else
            {
                isFacingRight = false;
                if (_bigScale.x > 0.0)
                {
                    _bigScale.x = -_bigScale.x;
                }
                if (_smallScale.x > 0.0)
                {
                    _smallScale.x = -_smallScale.x;
                }
            }
            startingScaleX = transform.localScale.x;
            startingScaleY = transform.localScale.y;
            if (!_isResizableSmall.Value)
            {
                targetScaleX = _bigScale.x;
                targetScaleY = _bigScale.y;
                currentSpeed = _myResizable.GetSmallSpeed();
                targetSpeed = _myResizable.GetBigSpeed();
            }
            else
            {
                targetScaleX = _smallScale.x;
                targetScaleY = _smallScale.y;
                currentSpeed = _myResizable.GetBigSpeed();
                targetSpeed = _myResizable.GetSmallSpeed();
            }
            float newResizeTime = proportion * Mathf.Abs(startingScaleY - targetScaleY);
            for (float t = 0; t < newResizeTime && myId == _resizeCoroutineId; t += Time.deltaTime)
            {
                interpolationPoint = t / newResizeTime;
                /*interpolationPoint = interpolationPoint * interpolationPoint * (3f - 2f * interpolationPoint);*/
                if (isFacingRight && transform.localScale.x < 0.0 || !isFacingRight && transform.localScale.x > 0.0) // used when character orientation flipped during shrinking
                {
                    isFacingRight = !isFacingRight;
                    targetScaleX = -targetScaleX;
                    startingScaleX = -startingScaleX;
                    _bigScale.x = -_bigScale.x;
                    _smallScale.x = -_smallScale.x;
                }
                currentScaleX = Mathf.Lerp(startingScaleX, targetScaleX, interpolationPoint);
                currentScaleY = Mathf.Lerp(startingScaleY, targetScaleY, interpolationPoint);
                transform.localScale = new Vector3(currentScaleX, currentScaleY, 0.0f);
                _myResizable.SetCurrentSpeed(Mathf.Lerp(currentSpeed, targetSpeed, interpolationPoint));
                yield return 0;
            }
            if (myId == _resizeCoroutineId)
            {
                transform.localScale = new Vector3(targetScaleX, targetScaleY, 0.0f);
                _myResizable.SetCurrentSpeed(targetSpeed);
            }
        }

        public void ChangeSize()
        {
            if (_isResizableSmall.Value)
            {
                StartCoroutine(Resize(_myResizable.GetFactor(), _myResizable.GetChangetime()));
            }
            else 
            {
                StartCoroutine(Resize(1 / _myResizable.GetFactor(), _myResizable.GetChangetime()));
            }
        }
    }
}
