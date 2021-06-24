using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "SpikeAttack", menuName = "Scriptable Objects/Spike Attack")]
    public class SpikeAttack : Attack
    {
        [SerializeField] private float _spikesGap;
        [SerializeField] private float _spikesSpeed;
        [SerializeField] private float _spikeSlideTime;
        [SerializeField] private float _spikeReturnDelay;
        [SerializeField] private float _spikeHeight;
        private List<SpikeController> _mySpikes;

        private IEnumerator SlideOutSpikes(float delay, System.Action attackCallback)
        {
            float spikeWaitTime = _attackRange / _spikesSpeed;
            yield return new WaitForSeconds(delay);
            foreach(SpikeController spike in _mySpikes)
            {
                spike.SlideOut(_spikeSlideTime, _spikeReturnDelay, _spikeHeight);
                yield return new WaitForSeconds(spikeWaitTime);
            }
            yield return new WaitForSeconds(2*_spikeSlideTime + _spikeReturnDelay);
            attackCallback();
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
                nextSpikePosition = new Vector3(nextSpikePosition.x + _spikesGap, nextSpikePosition.y, nextSpikePosition.z);
            }
        }

        public override void MakeAttack(System.Action attackCallback)
        {
            MonoBehaviour parentMonoBehaviour = _myParent.GetComponent<MonoBehaviour>();
            if(parentMonoBehaviour != null)
            {
                parentMonoBehaviour.StartCoroutine(SlideOutSpikes(_attackDelay, attackCallback));
            }
        }
    }
}
