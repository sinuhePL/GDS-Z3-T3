using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GDS3
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private BoolReference _isPlayerSmall;
        [SerializeField] private FloatReference _sizeChangeTime;
        [SerializeField] private FloatReference _sizeChangeFactor;
        [SerializeField] private Transform _followTarget;
        [SerializeField] private Vector3Reference _lastSpawnPoint;
        [SerializeField] private FloatReference _moveToSpawnPointTime;
        [Range(-10.0f, 10.0f)] [SerializeField] private float _xTargetOffset;
        [Range(-10.0f, 10.0f)] [SerializeField] private float _yTargetOffset;
        [Range(0.0f, 5.0f)] [SerializeField] private float _xTargetDeadZone;
        [Range(0.0f, 2.0f)] [SerializeField] private float _yTargetDeadZone;
        private bool _isFollowing;
        private int _resizeCoroutineId;
        private float _bigOrthographicSize;
        private float _smallOrthographicSize;
        private Vector2 _bigTargetOffset;
        private Vector2 _smallTargetOffset;
        private Vector2 _bigTargetDeadZone;
        private Vector2 _smallTargetDeadZone;


        private void Awake()
        {
            _isFollowing = true;
            DOTween.Init();
            _resizeCoroutineId = 0;
            if (_isPlayerSmall.Value)
            {
                _smallOrthographicSize = _camera.orthographicSize;
                _bigOrthographicSize = _camera.orthographicSize * _sizeChangeFactor.Value;
                _bigTargetOffset = new Vector2(_xTargetOffset, _yTargetOffset) * _sizeChangeFactor.Value;
                _smallTargetOffset = new Vector2(_xTargetOffset, _yTargetOffset);
                _bigTargetDeadZone = new Vector2(_xTargetDeadZone, _yTargetDeadZone) * _sizeChangeFactor.Value;
                _smallTargetDeadZone = new Vector2(_xTargetDeadZone, _yTargetDeadZone);

            }
            else
            {
                _smallOrthographicSize = _camera.orthographicSize/_sizeChangeFactor.Value;
                _bigOrthographicSize = _camera.orthographicSize;
                _bigTargetOffset = new Vector2(_xTargetOffset, _yTargetOffset);
                _smallTargetOffset = new Vector2(_xTargetOffset, _yTargetOffset) / _sizeChangeFactor.Value;
                _bigTargetDeadZone = new Vector2(_xTargetDeadZone, _yTargetDeadZone);
                _smallTargetDeadZone = new Vector2(_xTargetDeadZone, _yTargetDeadZone) / _sizeChangeFactor.Value;
            }
        }

        private IEnumerator Zoom(float factor)
        {
            float newSize, newXTargetOffset, newYTargetOffset, newXTargetDeadZone, newYTargetDeadZone;

            float proportion = _sizeChangeTime.Value / (_bigOrthographicSize - _smallOrthographicSize);
            float startingSize = _camera.orthographicSize;
            float startingXTargetOffset = _xTargetOffset;
            float startingYTargetOffset = _yTargetOffset;
            /*float startingXTargetDeadZone = _xTargetDeadZone;*/
            int myId = Random.Range(1, 999999999);
            if (_isPlayerSmall.Value)
            {
                newSize = _smallOrthographicSize;
                newXTargetOffset = _smallTargetOffset.x;
                newYTargetOffset = _smallTargetOffset.y;
                newXTargetDeadZone = _smallTargetDeadZone.x;
                newYTargetDeadZone = _smallTargetDeadZone.y;
            }
            else
            {
                newSize = _bigOrthographicSize;
                newXTargetOffset = _bigTargetOffset.x;
                newYTargetOffset = _bigTargetOffset.y;
                newXTargetDeadZone = _bigTargetDeadZone.x;
                newYTargetDeadZone = _bigTargetDeadZone.y;
            }
            _resizeCoroutineId = myId;
            _yTargetDeadZone = 0.0f;
            _xTargetDeadZone = 0.0f;
            float resizeTime = proportion * Mathf.Abs(newSize - startingSize);
            for (float t = 0; t < resizeTime && myId == _resizeCoroutineId; t += Time.deltaTime)
            {
                float interpolationPoint = t / resizeTime;
                /*interpolationPoint = interpolationPoint * interpolationPoint * (3f - 2f * interpolationPoint);*/
                _camera.orthographicSize = Mathf.Lerp(startingSize, newSize, interpolationPoint);
                _xTargetOffset = Mathf.Lerp(startingXTargetOffset, newXTargetOffset, interpolationPoint);
                _yTargetOffset = Mathf.Lerp(startingYTargetOffset, newYTargetOffset, interpolationPoint);
                /*_xTargetDeadZone = Mathf.Lerp(startingXTargetDeadZone, newXTargetDeadZone, interpolationPoint);*/
                yield return 0;
            }
            if (myId == _resizeCoroutineId)
            {
                _camera.orthographicSize = newSize;
                _xTargetOffset = newXTargetOffset;
                _yTargetOffset = newYTargetOffset;
                _xTargetDeadZone = newXTargetDeadZone;
                _yTargetDeadZone = newYTargetDeadZone;
            }
        }

        private void Update()
        {
            if (_isFollowing)
            {
                float newXCameraPosition = _followTarget.position.x + _xTargetOffset;
                float newYCameraPosition = _followTarget.position.y + _yTargetOffset;
                if (Mathf.Abs(newXCameraPosition - transform.position.x) < _xTargetDeadZone)
                {
                    newXCameraPosition = transform.position.x;
                }
                else
                {
                    if (newXCameraPosition > transform.position.x)
                    {
                        newXCameraPosition -= _xTargetDeadZone;
                    }
                    else
                    {
                        newXCameraPosition += _xTargetDeadZone;
                    }
                }
                if (Mathf.Abs(newYCameraPosition - transform.position.y) < _yTargetDeadZone && newYCameraPosition > transform.position.y)
                {
                    newYCameraPosition = transform.position.y;
                }
                else
                {
                    if (newYCameraPosition > transform.position.y)
                    {
                        newYCameraPosition -= _yTargetDeadZone;
                    }
                }
                transform.position = new Vector3(newXCameraPosition, newYCameraPosition, -10.0f);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(new Vector3(transform.position.x - _xTargetOffset, transform.position.y - _yTargetOffset + _yTargetDeadZone/2, 0.0f), new Vector3(_xTargetDeadZone, _yTargetDeadZone, 0.0f));
        }

        public void ChangeZoom()
        {
            if(!_isPlayerSmall.Value)
            {
                StartCoroutine(Zoom(_sizeChangeFactor.Value));
            }
            else
            {
                StartCoroutine(Zoom(1/_sizeChangeFactor.Value));
            }
        }

        public void MoveToNewPosition()
        {
            _isFollowing = false;
            Vector3 newPosition = new Vector3(_lastSpawnPoint.Value.x + _xTargetOffset, _lastSpawnPoint.Value.y + _yTargetOffset, _camera.transform.position.z);
            Tweener t = _camera.transform.DOMove(newPosition, _moveToSpawnPointTime.Value).SetEase(Ease.OutSine).OnComplete(()=> _isFollowing = true);
        }
    }
}
