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

        public void SlideOut()
        {
            Sequence spikeSequence = DOTween.Sequence();
            spikeSequence.Append(transform.DOScaleY(10.0f, 0.5f));
            spikeSequence.Insert(0.0f, transform.DOMoveY(transform.position.y + 0.5f, 0.5f));
            spikeSequence.Append(transform.DOScaleY(1.0f, 0.5f));
            spikeSequence.Insert(0.5f, transform.DOMoveY(transform.position.y - 0.5f, 0.5f));
        }
    }
}
