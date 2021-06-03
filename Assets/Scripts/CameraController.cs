﻿using System.Collections;
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
            float startingSize = _camera.orthographicSize;
            float newSize = _camera.orthographicSize * factor;
            float startingXTargetOffset = _xTargetOffset;
            float newXTargetOffset = _xTargetOffset * factor;
            float startingYTargetOffset = _yTargetOffset;
            float newYTargetOffset = _yTargetOffset * factor;
            float startingXTargetDeadZone = _xTargetDeadZone;
            float newXTargetDeadZone = _xTargetDeadZone * factor;
            float startingYTargetDeadZone = _yTargetDeadZone;
            float newYTargetDeadZone = _yTargetDeadZone * factor;
            for (float t = 0; t < _sizeChangeTime.Value; t += Time.deltaTime)
            {
                float interpolationPoint = t / _sizeChangeTime.Value;
                interpolationPoint = interpolationPoint * interpolationPoint * (3f - 2f * interpolationPoint);
                _camera.orthographicSize = Mathf.Lerp(startingSize, newSize, interpolationPoint);
                _xTargetOffset = Mathf.Lerp(startingXTargetOffset, newXTargetOffset, interpolationPoint);
                _yTargetOffset = Mathf.Lerp(startingYTargetOffset, newYTargetOffset, interpolationPoint);
                _xTargetDeadZone = Mathf.Lerp(startingXTargetDeadZone, newXTargetDeadZone, interpolationPoint);
                _yTargetDeadZone = Mathf.Lerp(startingYTargetDeadZone, newYTargetDeadZone, interpolationPoint);
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
