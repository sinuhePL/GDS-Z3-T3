using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class BranchController : MonoBehaviour
    {
        [SerializeField] private BoolReference _isPlayerSmall;
        [SerializeField] private Animator _myAnimator;
        [SerializeField] private EdgeCollider2D _myCollider;
        [SerializeField] private EdgeCollider2D _myBottomCollider;
        [SerializeField] private AudioSource _myAudioSource;
        [SerializeField] private Sound _bendingBranchSound;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _bendingBranchSoundVolume;
        [SerializeField] private Sound _bendingBranchBackSound;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _bendingBranchBackSoundVolume;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player" && !_isPlayerSmall.Value)
            {
                if (collision.otherCollider == (_myCollider as Collider2D))
                {
                    _myAnimator.SetTrigger("down");
                    _bendingBranchSound.Play(_myAudioSource, _bendingBranchSoundVolume);
                }
            }
        }

        public void DeactivateCollider()
        {
            _myCollider.enabled = false;
            _myBottomCollider.enabled = false;
            _myAnimator.ResetTrigger("down");
        }

        public void ActivateCollider()
        {
            _myCollider.enabled = true;
            _myBottomCollider.enabled = true;
        }

        public void PlayBendBackSound()
        {
            _bendingBranchBackSound.Play(_myAudioSource, _bendingBranchBackSoundVolume);
        }
    }
}
