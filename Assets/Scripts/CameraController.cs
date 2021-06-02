using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace GDS3
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private BoolReference _isSmallSize;
        [SerializeField] private FloatReference _sizeChangeTime;
        [SerializeField] private FloatReference _sizeChangeFactor;
        [SerializeField] private Transform _followTarget;
        [Range(-10.0f, 10.0f)] [SerializeField] private float _xTargetOffset;
        [Range(-10.0f, 10.0f)] [SerializeField] private float _yTargetOffset;
        [Range(0.0f, 5.0f)] [SerializeField] private float _xTargetDeadZone;
        [Range(0.0f, 2.0f)] [SerializeField] private float _yTargetDeadZone;

        private IEnumerator Zoom(float factor)
        {
            float newSize = _camera.orthographicSize * factor;
            float newXTargetOffset = _xTargetOffset * factor;
            float newYTargetOffset = _yTargetOffset * factor;
            float newXTargetDeadZone = _xTargetDeadZone * factor;
            float newYTargetDeadZone = _yTargetDeadZone * factor;
            for (float t = 0; t < _sizeChangeTime.Value; t += Time.deltaTime)
            {
                _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, newSize, t / _sizeChangeTime.Value);
                _xTargetOffset = Mathf.Lerp(_xTargetOffset, newXTargetOffset, t / _sizeChangeTime.Value);
                _yTargetOffset = Mathf.Lerp(_yTargetOffset, newYTargetOffset, t / _sizeChangeTime.Value);
                _xTargetDeadZone = Mathf.Lerp(_xTargetDeadZone, newXTargetDeadZone, t / _sizeChangeTime.Value);
                _yTargetDeadZone = Mathf.Lerp(_yTargetDeadZone, newYTargetDeadZone, t / _sizeChangeTime.Value);
                yield return 0;
            }
        }

        private void Update()
        {
            Vector3 targetScreenPosition = _camera.WorldToScreenPoint(_followTarget.position);
            Vector3 newCameraWorldPosition = _camera.ScreenToWorldPoint(targetScreenPosition);
            float newXCameraPosition = newCameraWorldPosition.x + _xTargetOffset;
            float newYCameraPosition = newCameraWorldPosition.y + _yTargetOffset;
            if(Mathf.Abs(newXCameraPosition - transform.position.x) < _xTargetDeadZone)
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
            if (Mathf.Abs(newYCameraPosition - transform.position.y) < _yTargetDeadZone)
            {
                newYCameraPosition = transform.position.y;
            }
            else
            {
                if (newYCameraPosition > transform.position.y)
                {
                    newYCameraPosition -= _yTargetDeadZone;
                }
                else
                {
                    newYCameraPosition += _yTargetDeadZone;
                }
            }
            transform.position = new Vector3(newXCameraPosition, newYCameraPosition, -10.0f); ;
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
    }
}
