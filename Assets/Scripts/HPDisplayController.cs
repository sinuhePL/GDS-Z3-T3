using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class HPDisplayController : MonoBehaviour
    {
        public IntegerReference _initialHP;
        public IntegerReference _currentHP;
        public GameObject _hitPointMarkerPrefab;

        private List<ParticleSystem> _hitPointsMarkers;

        private void Start()
        {
            _hitPointsMarkers = new List<ParticleSystem>();
            for(int i = 0; i < _initialHP.Value; i++)
            {
                _hitPointsMarkers.Add(Instantiate(_hitPointMarkerPrefab, transform.position + new Vector3(Mathf.Cos(2 * 3.1415f * (i+1)/_initialHP.Value), Mathf.Sin(2 * 3.1415f * (i+1) / _initialHP.Value) + 3.0f, 0.0f), Quaternion.identity, transform).GetComponent<ParticleSystem>());
            }
        }

        public void UpdateDisplay()
        {
            ParticleSystem systemToDestroy;

            systemToDestroy = _hitPointsMarkers[_hitPointsMarkers.Count - 1];
            _hitPointsMarkers.RemoveAt(_hitPointsMarkers.Count - 1);
            Destroy(systemToDestroy);
        }

        private void Update()
        {
            float currentTime = Time.time;
            int i = 0;
            foreach(ParticleSystem hitPoint in _hitPointsMarkers)
            {
                hitPoint.transform.localPosition = new Vector3(Mathf.Cos(currentTime%(2 * 3.1415f) + 2 * 3.1415f * (i+1) / _initialHP.Value), Mathf.Sin(currentTime%(2 * 3.1415f) + 2 * 3.1415f * (i+1) / _initialHP.Value) * 0.3f + 4.0f, 0.0f) * 5.0f;
                i++;
            }
        }
    }
}
