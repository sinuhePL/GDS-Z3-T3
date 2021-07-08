﻿using System.Collections;
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
        [SerializeField] private Transform _handTransform;
        [SerializeField] private Transform _interactionCheck;
        [SerializeField] private LayerMask _resizeBlocker;
        [SerializeField] private Transform _heightCheck;
        [SerializeField] private State _startingState;
        [SerializeField] private UnityEvent _hitEvent;
        [SerializeField] private UnityEvent _killedEvent;
        private CharacterMovementController _myMovement;
        private CharacterJumpController _myJump;
        private float _attackLength;
        private bool _isGamePaused;
        private float _prevAnimationSpeed;
        private Vector3 _prevVelocity;
        private bool _wasKinematic;

        private void Awake()
        {
            _myMovement = new CharacterMovementController(_myBody, _isFacingRight);
            _myJump = new CharacterJumpController(_myBody);
            _isGamePaused = false;
            _prevAnimationSpeed = 1.0f;
            _prevVelocity = Vector3.zero;
            _wasKinematic = _myBody.isKinematic;
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
            if (!_isGamePaused)
            {
                _myMovement.Move(moveSpeed);
                _myAnimator.SetFloat("walk_speed", Mathf.Abs(moveSpeed));
            }
            else
            {
                _myMovement.Move(0.0f);
            }
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

        public Transform GetInteractionCheck()
        {
            return _interactionCheck;
        }

        public Transform GetHandTransform()
        {
            return _handTransform;
        }

        public void PauseCharacter()
        {
            if(_isGamePaused)
            {
                _myAnimator.speed = _prevAnimationSpeed;
                _myBody.velocity = _prevVelocity;
                _myBody.isKinematic = _wasKinematic;
                _isGamePaused = false;
            }
            else
            {
                _prevAnimationSpeed = _myAnimator.speed;
                _myAnimator.speed = 0.0f;
                _prevVelocity = _myBody.velocity;
                _myBody.velocity = Vector3.zero;
                _wasKinematic = _myBody.isKinematic;
                _myBody.isKinematic = true;
                _isGamePaused = true;
            }
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
