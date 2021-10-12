using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GDS3
{
    public class ScrollingController : MonoBehaviour
    {
        [SerializeField] private Vector3Reference _cameraSpeed;
        [SerializeField] private float _speedMultiplier;
        [SerializeField] private FloatReference _moveToSpawnPointTime;
        public bool _isScrolling;
        private Vector3 _backPosition;

        private void Start()
        {
            SetBackPosition();
        }

        private void Update()
        {
            if (_isScrolling)
            {
                transform.position += new Vector3(_cameraSpeed.Value.x * _speedMultiplier * Time.deltaTime, _cameraSpeed.Value.y * _speedMultiplier * Time.deltaTime, 0.0f);
            }
        }

        public void MoveBack()
        {
            bool wasScrolling = _isScrolling;
            _isScrolling = false;
            transform.DOMove(_backPosition, _moveToSpawnPointTime.Value).SetEase(Ease.OutSine).OnComplete(() => _isScrolling = wasScrolling);
        }

        public void SetBackPosition()
        {
            _backPosition = transform.position;
        }
    }
}
