using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace GDS3
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private BoolReference _isSmallSize;
        [SerializeField] private FloatReference _sizeChangeTime;
        [SerializeField] private FloatReference _sizeChangeFactor;

        private IEnumerator Zoom(float factor)
        {
            CinemachineFramingTransposer transposer;

            float newSize = _camera.orthographicSize * factor;
            transposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            float Xdamping = transposer.m_XDamping;
            float Ydamping = transposer.m_YDamping;
            float deadZoneHeight = transposer.m_DeadZoneHeight;
            float deadZoneWidth = transposer.m_DeadZoneWidth;
            transposer.m_XDamping = 0.0f;
            transposer.m_YDamping = 0.0f;
            transposer.m_DeadZoneHeight = 0.0f;
            transposer.m_DeadZoneWidth = 0.0f;
            for (float t = 0; t < _sizeChangeTime.Value; t += Time.deltaTime)
            {
                _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_virtualCamera.m_Lens.OrthographicSize, newSize, t / _sizeChangeTime.Value);
                yield return 0;
            }
            transposer.m_XDamping = Xdamping;
            transposer.m_YDamping = Ydamping;
            transposer.m_DeadZoneHeight = deadZoneHeight;
            transposer.m_DeadZoneWidth = deadZoneWidth;
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
