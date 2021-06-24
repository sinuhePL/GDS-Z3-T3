using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace GDS3
{
    public class SpikeController : MonoBehaviour
    {
        [SerializeField] private UnityEvent _playerHit;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                _playerHit.Invoke();
            }
        }

        public void SlideOut(float slideTime, float returnDelay, float spikeHeight)
        {
            float startingYPosition = transform.position.y;
            Sequence spikeSequence = DOTween.Sequence();
            spikeSequence.Append(transform.DOScaleY(spikeHeight*10, slideTime));
            spikeSequence.Insert(0.0f, transform.DOMoveY(transform.position.y + spikeHeight/2.0f, slideTime));
            spikeSequence.Append(transform.DOScaleY(0.0f, slideTime).SetDelay(returnDelay));
            spikeSequence.Insert(slideTime, transform.DOMoveY(startingYPosition, slideTime).SetDelay(returnDelay));
        }
    }
}
