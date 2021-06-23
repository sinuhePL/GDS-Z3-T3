using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "SpikeAttack", menuName = "Scriptable Objects/Spike Attack")]
    public class SpikeAttack : Attack
    {
        [SerializeField] private float _spikeDistance;
        [SerializeField] private float _spikesSpeed;
        private List<SpikeController> _mySpikes;

        private IEnumerator SlideOutSpikes()
        {
            foreach(SpikeController spike in _mySpikes)
            {
                spike.SlideOut();
                yield return new WaitForSeconds(_spikesSpeed);
            }
        }

        public override void Initialize(Transform attackTransform, GameObject myParent)
        {
            Vector3 nextSpikePosition;
            _attackPoint = attackTransform;
            _myParent = myParent;
            _mySpikes = new List<SpikeController>();
            nextSpikePosition = attackTransform.position;
            while(nextSpikePosition.x < _attackPoint.position.x + _attackRange)
            {
                GameObject newSpike = Instantiate(_weaponPrefab, nextSpikePosition, Quaternion.identity, myParent.transform);
                SpikeController newSpikeController = newSpike.GetComponent<SpikeController>();
                if(newSpikeController != null)
                {
                    _mySpikes.Add(newSpikeController);
                }
                nextSpikePosition = new Vector3(nextSpikePosition.x + _spikeDistance, nextSpikePosition.y, nextSpikePosition.z);
            }
        }

        public override bool MakeAttack()
        {
            MonoBehaviour parentMonoBehaviour = _myParent.GetComponent<MonoBehaviour>();
            if(parentMonoBehaviour != null)
            {
                parentMonoBehaviour.StartCoroutine(SlideOutSpikes());
            }
            return false;
        }
    }
}
