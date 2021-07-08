using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace GDS3
{
    public class SpikeController : MonoBehaviour
    {
        [SerializeField] private LayerMask _hitMask;
        private bool _isGamePaused;

        private void Awake()
        {
            _isGamePaused = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IHitable myHit;
            bool isTarget = _hitMask == (_hitMask | (1 << collision.gameObject.layer));
            if (isTarget)
            {
                myHit = collision.gameObject.GetComponent<IHitable>();
                if (myHit != null)
                {
                    myHit.Hit();
                }
            }
        }

        public void OnPausePressed()
        {
            if (_isGamePaused)
            {
                DOTween.PlayAll();
                _isGamePaused = false;
            }
            else
            {
                DOTween.PauseAll();
                _isGamePaused = true;
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
