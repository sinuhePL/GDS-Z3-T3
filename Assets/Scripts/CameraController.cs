using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private BoolReference _isSmallSize;
        [SerializeField] private FloatReference _sizeChangeTime;
        [SerializeField] private FloatReference _sizeChangeFactor;
        [SerializeField] private Vector3Reference _sizeChangePosition;

        private IEnumerator Zoom(float factor, Vector3 zoomPosition)
        {
            float newSize = _camera.orthographicSize * factor;
            Vector3 zoomScreenPosition = _camera.WorldToScreenPoint(zoomPosition);
            for (float t = 0; t < _sizeChangeTime.Value; t += Time.deltaTime)
            {
                _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, newSize, t / _sizeChangeTime.Value);
                transform.position += zoomPosition - _camera.ScreenToWorldPoint(zoomScreenPosition);
                yield return 0;
            }
        }

        public void ChangeZoom()
        {
            if(_isSmallSize.Value)
            {
                StartCoroutine(Zoom(1/_sizeChangeFactor.Value, _sizeChangePosition.Value));
            }
            else
            {
                StartCoroutine(Zoom(_sizeChangeFactor.Value, _sizeChangePosition.Value));
            }
        }
    }
}
