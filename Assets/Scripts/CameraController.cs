using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GDS3
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private BoolReference _isSmallSize;
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

        private void Awake()
        {
            _isFollowing = true;
            DOTween.Init();
        }

        private IEnumerator Zoom(float factor)
        {
            float startingSize = _camera.orthographicSize;
            float newSize = _camera.orthographicSize * factor;
            float startingXTargetOffset = _xTargetOffset;
            float newXTargetOffset = _xTargetOffset * factor;
            float startingYTargetOffset = _yTargetOffset;
            float newYTargetOffset = _yTargetOffset * factor;
            /*float startingXTargetDeadZone = _xTargetDeadZone;*/
            float newXTargetDeadZone = _xTargetDeadZone * factor;
            float newYTargetDeadZone = _yTargetDeadZone * factor;
            _yTargetDeadZone = 0.0f;
            _xTargetDeadZone = 0.0f;
            for (float t = 0; t < _sizeChangeTime.Value; t += Time.deltaTime)
            {
                float interpolationPoint = t / _sizeChangeTime.Value;
                interpolationPoint = interpolationPoint * interpolationPoint * (3f - 2f * interpolationPoint);
                _camera.orthographicSize = Mathf.Lerp(startingSize, newSize, interpolationPoint);
                _xTargetOffset = Mathf.Lerp(startingXTargetOffset, newXTargetOffset, interpolationPoint);
                _yTargetOffset = Mathf.Lerp(startingYTargetOffset, newYTargetOffset, interpolationPoint);
                /*_xTargetDeadZone = Mathf.Lerp(startingXTargetDeadZone, newXTargetDeadZone, interpolationPoint);*/
                yield return 0;
            }
            _camera.orthographicSize = newSize;
            _xTargetOffset = newXTargetOffset;
            _yTargetOffset = newYTargetOffset;
            _xTargetDeadZone = newXTargetDeadZone;
            _yTargetDeadZone = newYTargetDeadZone;
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
            if(_isSmallSize.Value)
            {
                StartCoroutine(Zoom(1/_sizeChangeFactor.Value));
            }
            else
            {
                StartCoroutine(Zoom(_sizeChangeFactor.Value));
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
