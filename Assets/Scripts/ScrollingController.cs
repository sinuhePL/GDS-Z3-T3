using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class ScrollingController : MonoBehaviour
    {
        [SerializeField] private Vector3Reference _cameraSpeed;
        [SerializeField] private float _speedMultiplier;
        public bool _isScrolling;

        private void Update()
        {
            if (_isScrolling)
            {
                transform.position += new Vector3(_cameraSpeed.Value.x * _speedMultiplier * Time.deltaTime, _cameraSpeed.Value.y * _speedMultiplier * Time.deltaTime, 0.0f);
            }
        }
    }
}
