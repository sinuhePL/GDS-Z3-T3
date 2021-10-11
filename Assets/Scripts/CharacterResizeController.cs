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
        [SerializeField] private ParticleSystem _fireParticleSystem;
        [SerializeField] private ParticleSystem _smokeParticleSystem;
        [SerializeField] private ParticleSystem _sparksParticleSystem;
        [SerializeField] private Light _lampLight;
        [SerializeField] private PlayerSoundController _mySoundController;
        private int _resizeCoroutineId;
        private Vector3 _bigScale;
        private Vector3 _smallScale;
        private IResizable _myResizable;
        private float _smallScalePS;
        private float _bigScalePS;
        private float _smallLighIntensity;
        private float _bigLightIntensity;

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
                    _smallScalePS = _fireParticleSystem.transform.localScale.x;
                    _bigScalePS = _fireParticleSystem.transform.localScale.x * _myResizable.GetFactor();
                    _smallLighIntensity = _lampLight.intensity;
                    _bigLightIntensity = _lampLight.intensity * _myResizable.GetFactor();
                }
                else
                {
                    _smallScale = transform.localScale / _myResizable.GetFactor();
                    _bigScale = transform.localScale;
                    _smallScalePS = _fireParticleSystem.transform.localScale.x / _myResizable.GetFactor();
                    _bigScalePS = _fireParticleSystem.transform.localScale.x;
                    _smallLighIntensity = _lampLight.intensity / _myResizable.GetFactor();
                    _bigLightIntensity = _lampLight.intensity;
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
            float startingPSScale, targetPSScale, currentPSScale;
            float startingLightIntensity, targetLightIntensity;

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
            startingPSScale = _fireParticleSystem.transform.localScale.x;
            startingLightIntensity = _lampLight.intensity;
            if (!_isResizableSmall.Value)
            {
                targetScaleX = _bigScale.x;
                targetScaleY = _bigScale.y;
                currentSpeed = _myResizable.GetSmallSpeed();
                targetSpeed = _myResizable.GetBigSpeed();
                targetPSScale = _bigScalePS;
                targetLightIntensity = _bigLightIntensity;
            }
            else
            {
                targetScaleX = _smallScale.x;
                targetScaleY = _smallScale.y;
                currentSpeed = _myResizable.GetBigSpeed();
                targetSpeed = _myResizable.GetSmallSpeed();
                targetPSScale = _smallScalePS;
                targetLightIntensity = _smallLighIntensity;
            }
            float newResizeTime = proportion * Mathf.Abs(startingScaleY - targetScaleY);
            for (float t = 0; t < newResizeTime && myId == _resizeCoroutineId; t += Time.deltaTime)
            {
                interpolationPoint = t / newResizeTime;
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
                currentPSScale = Mathf.Lerp(startingPSScale, targetPSScale, interpolationPoint);
                transform.localScale = new Vector3(currentScaleX, currentScaleY, 1.0f);
                _myResizable.SetCurrentSpeed(Mathf.Lerp(currentSpeed, targetSpeed, interpolationPoint));
                _fireParticleSystem.transform.localScale = new Vector3(currentPSScale, currentPSScale, currentPSScale);
                _smokeParticleSystem.transform.localScale = new Vector3(currentPSScale, currentPSScale, currentPSScale);
                _sparksParticleSystem.transform.localScale = new Vector3(currentPSScale, currentPSScale, currentPSScale);
                _lampLight.intensity = Mathf.Lerp(startingLightIntensity, targetLightIntensity, interpolationPoint);
                yield return 0;
            }
            if (myId == _resizeCoroutineId)
            {
                transform.localScale = new Vector3(targetScaleX, targetScaleY, 1.0f);
                _myResizable.SetCurrentSpeed(targetSpeed);
                _fireParticleSystem.transform.localScale = new Vector3(targetPSScale, targetPSScale, targetPSScale);
                _smokeParticleSystem.transform.localScale = new Vector3(targetPSScale, targetPSScale, targetPSScale);
                _sparksParticleSystem.transform.localScale = new Vector3(targetPSScale, targetPSScale, targetPSScale);
                _lampLight.intensity = targetLightIntensity;
            }
        }

        public void ChangeSize()
        {
            if (_isResizableSmall.Value)
            {
                StartCoroutine(Resize(_myResizable.GetFactor(), _myResizable.GetChangetime()));
                _mySoundController.PlayShrinkSound();
            }
            else 
            {
                StartCoroutine(Resize(1 / _myResizable.GetFactor(), _myResizable.GetChangetime()));
                _mySoundController.PlayEnlargeSound();
            }
        }
    }
}
