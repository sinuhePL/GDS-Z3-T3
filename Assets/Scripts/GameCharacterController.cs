using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class GameCharacterController : MonoBehaviour, IControllable, IHitable
    {
        [SerializeField] private CharacterBrain _myBrain;
        [SerializeField] private Attack _myAttack;
        [SerializeField] private Animator _myAnimator;
        [SerializeField] private Rigidbody2D _myBody;
        [SerializeField] private bool _isFacingRight;
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private Transform _hitCheck;
        [SerializeField] private LayerMask _resizeBlocker;
        [SerializeField] private Transform _heightCheck;
        [SerializeField] private State _startingState;
        [SerializeField] private UnityEvent _hitEvent;
        [SerializeField] private UnityEvent _killedEvent;
        private CharacterMovementController _myMovement;
        private CharacterJumpController _myJump;
        private float _attackLength;

        private void Awake()
        {
            _myMovement = new CharacterMovementController(_myBody, _isFacingRight);
            _myJump = new CharacterJumpController(_myBody);
        }

        private void Start()
        {
            if (_myBrain != null)
            {
                _myBrain.Initialize(this);
            }
            if (_myAttack != null)
            {
                _myAttack.Initialize(_hitCheck, gameObject);
            }
            _attackLength = 0.0f;
            AnimationClip[] clips = _myAnimator.runtimeAnimatorController.animationClips;
            foreach(AnimationClip clip in clips)
            {
                if(clip.name.EndsWith("attack"))
                {
                    _attackLength = clip.length;
                }
            }
        }

        private void Update()
        {
            if (_myBrain != null)
            {
                _myBrain.ThinkAboutAnimation();
            }
        }

        private void FixedUpdate()
        {
            if (_myBrain != null)
            {
                _myBrain.ThinkAboutPhysics();
                _myAnimator.SetFloat("jump_speed", _myBody.velocity.y);
            }
        }

        private void OnDrawGizmos()
        {
            if (_myBrain != null)
            {
                _myBrain.DrawGizmo(transform.position);
            }
            if (_hitCheck != null)
            {
                Gizmos.color = Color.red; ;
                Gizmos.DrawWireSphere(_hitCheck.position, _myAttack._attackRange);
            }
        }

        public void AttackEnd()
        {
            _myAnimator.ResetTrigger("attack");
            _myBrain._isAttackFinished = true;
        }

        public Rigidbody2D GetRigidbody2D()
        {
            return _myBody;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void MoveMe(float moveSpeed)
        {
            _myMovement.Move(moveSpeed);
            _myAnimator.SetFloat("walk_speed", Mathf.Abs(moveSpeed));
        }

        public void Jump(float jumpYVelocity)
        {
            _myJump.Jump(jumpYVelocity);
        }

        public void Attack()
        {
            _myAnimator.SetTrigger("attack");
            _myBrain._currentCooldownTime = _attackLength;
            _myBrain._isAttackFinished = false;
            _myAttack.MakeAttack(_attackLength, AttackEnd);
        }

        public void Dash()
        {
            _myAnimator.SetTrigger("dash");
        }

        public LayerMask GetGroundMask()
        {
            return _whatIsGround;
        }

        public Transform GetGroundCheck()
        {
            return _groundCheck;
        }

        public Transform GetHitCheck()
        {
            return _hitCheck;
        }

        public LayerMask GetResizeBlockerMask()
        {
            return _resizeBlocker; ;
        }

        public Transform GetHeightCheck()
        {
            return _heightCheck;
        }

        public State GetStartingState()
        {
            return _startingState;
        }

        public void Hit()
        {
            _myBrain._currentHitPoints--;
            Debug.Log("Pozostało życia: " + _myBrain._currentHitPoints);
            if(_myBrain._currentHitPoints < 1)
            {
                _killedEvent.Invoke();
            }
            else
            {
                _hitEvent.Invoke();
            }
        }
    }
}
