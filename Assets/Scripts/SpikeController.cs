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
        [SerializeField] private AudioSource _myAudioSource;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private ParticleSystem _dirtBurst;
        [SerializeField] private Sound _spikeSound;
        [Range(0.0f, 1.0f)] public float _spikeVolume;
        private bool _isGamePaused;
        private bool _allowFixedUpdate;

        private void Awake()
        {
            _isGamePaused = false;
            _allowFixedUpdate = true;
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

        private void FixedUpdate()
        {
            if (_allowFixedUpdate)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0.0f, 2.0f, 0.0f), transform.TransformDirection(Vector3.down), 3.0f, _whatIsGround);
                if (hit.collider != null && hit.distance > 0.0f)
                {
                    Debug.DrawRay(transform.position + new Vector3(0.0f, 2.0f, 0.0f), transform.TransformDirection(Vector3.down) * 3.0f, Color.yellow);
                    transform.position = new Vector3(transform.position.x, transform.position.y - hit.distance + 2.0f - 0.3f, transform.position.z);
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
            _allowFixedUpdate = false;
            float startingYPosition = transform.position.y;
            _dirtBurst.Play();
            Sequence spikeSequence = DOTween.Sequence();
            _spikeSound.Play(_myAudioSource, _spikeVolume);
            spikeSequence.Append(transform.DOScaleY(spikeHeight*10, slideTime));
            spikeSequence.Insert(0.0f, transform.DOMoveY(transform.position.y + spikeHeight/2.0f, slideTime));
            spikeSequence.Append(transform.DOScaleY(0.0f, slideTime).SetDelay(returnDelay));
            spikeSequence.Insert(slideTime, transform.DOMoveY(startingYPosition, slideTime).SetDelay(returnDelay).OnComplete(() => { _allowFixedUpdate = true; _dirtBurst.Stop(); }));
        }
    }
}
