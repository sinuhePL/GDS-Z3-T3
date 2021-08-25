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

        private ParticleSystem[] _hitPointsMarkers;

        private void Start()
        {
            _hitPointsMarkers = new ParticleSystem[_initialHP.Value];
            for(int i = 0; i < _initialHP.Value; i++)
            {
                _hitPointsMarkers[i] = Instantiate(_hitPointMarkerPrefab, transform.position + new Vector3(i-1, 3.0f, 0.0f), Quaternion.identity, transform).GetComponent<ParticleSystem>();
            }
        }

        public void UpdateDisplay()
        { 
            for (int i = 0; i < _initialHP.Value; i++)
            {
                if(i == _currentHP.Value)
                {
                    Destroy(_hitPointsMarkers[i]);
                }
            }
        }
    }
}
