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
            float elapsedTime = 0.0f;
            float currentTime;
            float spikeWaitTime = _attackRange / _spikesSpeed;
            yield return new WaitForSeconds(delay);
            for(int i = 0; i < _mySpikes.Count; i++)
            {
                if (_isGamePaused)
                {
                    i--;
                    yield return null;
                }
                else
                {
                    _mySpikes[i].SlideOut(_spikeSlideTime, _spikeReturnDelay, _spikeHeight);
                    yield return new WaitForSeconds(spikeWaitTime);
                }
            }
            currentTime = Time.time;
            while(elapsedTime < 2 * _spikeSlideTime + _spikeReturnDelay)
            {
                if (!_isGamePaused)
                {
                    elapsedTime += Time.time - currentTime;
                }
                currentTime = Time.time;
                yield return null;
            }
            attackCallback();
        }

        public override void Initialize(Transform attackTransform, GameObject myParent)
        {
            base.Initialize(attackTransform, myParent);
            Vector3 nextSpikePosition;
            bool reverseScale = false;
            _attackPoint = attackTransform;
            _myParent = myParent;
            _mySpikes = new List<SpikeController>();
            nextSpikePosition = attackTransform.position;
            while(nextSpikePosition.x < _attackPoint.position.x + _attackRange)
            {
                GameObject newSpike = Instantiate(_weaponPrefab, nextSpikePosition, Quaternion.identity, myParent.transform);
                if(reverseScale)
                {
                    newSpike.transform.localScale = new Vector3(-newSpike.transform.localScale.x, newSpike.transform.localScale.y, newSpike.transform.localScale.z);
                    reverseScale = false;
                }
                else
                {
                    reverseScale = true;
                }
                SpikeController newSpikeController = newSpike.GetComponent<SpikeController>();
                if(newSpikeController != null)
                {
                    _mySpikes.Add(newSpikeController);
                }
                nextSpikePosition = new Vector3(nextSpikePosition.x + _spikesGap, nextSpikePosition.y, nextSpikePosition.z);
            }
        }

        public override void MakeAttack(float attackDuration, System.Action attackCallback)
        {
            MonoBehaviour parentMonoBehaviour = _myParent.GetComponent<MonoBehaviour>();
            if(parentMonoBehaviour != null)
            {
                parentMonoBehaviour.StartCoroutine(SlideOutSpikes(_attackDelay, attackCallback));
            }
        }
    }
}
