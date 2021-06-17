using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class CharacterResizeController : MonoBehaviour
    {
        [SerializeField] private CharacterBrain _characterBrain;
        private int _resizeCoroutineId;
        private Vector3 _bigScale;
        private Vector3 _smallScale;

        private void Awake()
        {
            _characterBrain._isCharacterSmall.Value = false;
            _resizeCoroutineId = 0;
            Random.InitState((int)System.DateTime.Now.Ticks);
            if(_characterBrain._isCharacterSmall.Value)
            {
                _smallScale = transform.localScale;
                _bigScale = transform.localScale * _characterBrain._sizeChangeFactor.Value;
            }
            else
            {
                _smallScale = transform.localScale / _characterBrain._sizeChangeFactor.Value;
                _bigScale = transform.localScale;
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
            }
            else
            {
                isFacingRight = false;
            }
            startingScaleX = transform.localScale.x;
            startingScaleY = transform.localScale.y;
            if (!_characterBrain._isCharacterSmall.Value)
            {
                targetScaleX = _bigScale.x;
                targetScaleY = _bigScale.y;
                currentSpeed = _characterBrain._smallMovementSpeed.Value;
                targetSpeed = _characterBrain._bigMovementSpeed.Value;
            }
            else
            {
                targetScaleX = _smallScale.x;
                targetScaleY = _smallScale.y;
                currentSpeed = _characterBrain._bigMovementSpeed.Value;
                targetSpeed = _characterBrain._smallMovementSpeed.Value;
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
                }
                currentScaleX = Mathf.Lerp(startingScaleX, targetScaleX, interpolationPoint);
                currentScaleY = Mathf.Lerp(startingScaleY, targetScaleY, interpolationPoint);
                transform.localScale = new Vector3(currentScaleX, currentScaleY, 0.0f);
                _characterBrain._currentMovementSpeed = Mathf.Lerp(currentSpeed, targetSpeed, interpolationPoint);
                yield return 0;
            }
            if (myId == _resizeCoroutineId)
            {
                transform.localScale = new Vector3(targetScaleX, targetScaleY, 0.0f);
                _characterBrain._currentMovementSpeed = targetSpeed;
            }
        }

        public void ChangeSize()
        {
            if (_characterBrain._isCharacterSmall.Value)
            {
                StartCoroutine(Resize(_characterBrain._sizeChangeFactor.Value, _characterBrain._sizeChangeTime.Value));
            }
            else 
            {
                StartCoroutine(Resize(1 / _characterBrain._sizeChangeFactor.Value, _characterBrain._sizeChangeTime.Value));
            }
        }
    }
}
