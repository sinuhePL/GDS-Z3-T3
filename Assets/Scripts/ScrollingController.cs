using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class ScrollingController : MonoBehaviour
    {
        [SerializeField] private Vector3Reference _cameraSpeed;
        [SerializeField] private float _speedMultiplier;

        private void Update()
        {
            transform.position += new Vector3(_cameraSpeed.Value.x * _speedMultiplier * Time.deltaTime, _cameraSpeed.Value.y * _speedMultiplier * Time.deltaTime, 0.0f);
        }
    }
}
