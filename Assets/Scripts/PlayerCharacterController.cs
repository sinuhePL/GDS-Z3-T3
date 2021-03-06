using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace GDS3
{
    public class PlayerCharacterController : MonoBehaviour, IHitable, IResizable, PlayerInput.IGameplayActions
    {
        [Header("Movement Ability")]
        public FloatReference _bigMovementSpeed;
        public FloatReference _smallMovementSpeed;
        [HideInInspector] public float _currentMovementSpeed;
        [HideInInspector] public CharacterMovementController _myMovement;
        [HideInInspector] public float _movementValue;
        [Header("Jump Ability")]
        public FloatReference _jumpTime;
        public FloatReference _jumpHeight;
        public FloatReference _jumpSmallModifier;
        public FloatReference _currentVerticalSpeed;
        public float _groundCheckRadius;
        public LayerMask _whatIsGround;
        public Transform _groundCheck;
        [HideInInspector] public CharacterJumpController _myJump;
        [Header("Dash Ability")]
        public FloatReference _dashVelocity;
        public FloatReference _dashDistance;
        public FloatReference _dashCooldownTime;
        public Transform _dashCheck;
        public ParticleSystem _dashParticleSystem;
        [HideInInspector] public float _lastDashTime;
        public LayerMask _dashObstacles;
        [Header("Attack Ability")]
        public Attack _myAttack;
        public Transform _hitCheck;
        [HideInInspector] public Vector3 _attackEndPosition;
        [HideInInspector] public float _attackLength;
        [Header("Resize Ability")]
        public BoolReference _isPlayerSmall;
        public FloatReference _sizeChangeFactor;
        public FloatReference _sizeChangeTime;
        public LayerMask _resizeBlockerMask;
        public LayerMask _deadlyForSmallMask;
        public Transform _heightCheck;
        public string _resizeBlockedStatement;
        public UnityEvent _sizeChangeEvent;
        [Header("Interaction Ability")]
        public FloatReference _interactionRange;
        public Transform[] _interactionChecks;
        public LayerMask _interactionMask;
        public Transform _handTransform;
        public ParticleSystem _sacrificeFire;
        [HideInInspector] public Transform _pocket;
        [Header("Other")]
        public Vector3Reference _lastSpawPoint;
        public IntegerReference _hitPoints;
        public BoolReference _isInputBlocked;
        public UnityEvent _hitEvent;
        public UnityEvent _pauseEvent;
        public UnityEvent _menuEvent;
        public Animator _myAnimator;
        public Rigidbody2D _myBody;
        public bool _isFacingRight;
        public MonologController _myMonolog;
        public ParticleSystem _lampParticleSystem;
        [HideInInspector] public float _currentCooldownTime;
        [HideInInspector] public bool _isGamePaused;

        private PlayerCharacterState _myState;
        protected PlayerInput _playerInput;
        private float _prevAnimationSpeed;
        private Vector3 _prevVelocity;
        private bool _wasKinematic;
        private int _currentHitPoints;

        private void Awake()
        {
            _myMovement = new CharacterMovementController(_myBody, _isFacingRight, _whatIsGround);
            _myJump = new CharacterJumpController(_myBody);
            if (_jumpHeight.Value > 0.0f && _jumpTime.Value > 0.0f)
            {
                float gForce = ((2 * _jumpHeight.Value) / (_jumpTime.Value * _jumpTime.Value)) / 9.81f;
                _myBody.gravityScale = gForce;
            }
            if (_myAttack != null)
            {
                _myAttack.Initialize(_hitCheck, gameObject);
            }
            _attackLength = 0.0f;
            AnimationClip[] clips = _myAnimator.runtimeAnimatorController.animationClips;
            foreach (AnimationClip clip in clips)
            {
                if (clip.name.EndsWith("attack"))
                {
                    _attackLength = clip.length / 1.5f;
                }
            }
            _isGamePaused = false;
            _isInputBlocked.Value = true;
            _currentHitPoints = _hitPoints.Value;
            _playerInput = new PlayerInput();
            _playerInput.Gameplay.SetCallbacks(this);
            _myState = new PlayerOnGroundState(this);
            _pocket = null;
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void Start()
        {
            if (_isPlayerSmall.Value)
            {
                _currentMovementSpeed = _smallMovementSpeed.Value;
            }
            else
            {
                _currentMovementSpeed = _bigMovementSpeed.Value;
            }
        }

        private void FixedUpdate()
        {
            PlayerCharacterState newState = _myState.StateFixedUpdate();
            if (newState != null)
            {
                _myState = newState;
            }
        }

        private void OnDrawGizmos()
        {
            if (_myState != null)
            {
                _myState.DrawGizmo();
            }
            if (_hitCheck != null)
            {
                Gizmos.color = Color.red; ;
                Gizmos.DrawWireSphere(_hitCheck.position, _myAttack._attackRange);
            }
            foreach(Transform check in _interactionChecks)
            {
                Gizmos.color = Color.white;
                if (_isPlayerSmall.Value)
                {
                    Gizmos.DrawWireSphere(check.position, _interactionRange.Value/_sizeChangeFactor.Value);
                }
                else
                {
                    Gizmos.DrawWireSphere(check.position, _interactionRange.Value);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Monolog")
            {
                MonologTriggerController monologTrigger = collision.gameObject.GetComponent<MonologTriggerController>();
                if (monologTrigger != null && !monologTrigger.CheckIfVisited())
                {
                    _movementValue = 0.0f;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            bool isDeadly = _deadlyForSmallMask == (_deadlyForSmallMask | (1 << collision.gameObject.layer));
            if (isDeadly && _isPlayerSmall.Value)
            {
                Hit();
            }
        }

        public float GetFactor()
        {
            return _sizeChangeFactor.Value;
        }

        public float GetSmallSpeed()
        {
            return _smallMovementSpeed.Value;
        }

        public float GetBigSpeed()
        {
            return _bigMovementSpeed.Value;
        }

        public void SetCurrentSpeed(float speed)
        {
            _currentMovementSpeed = speed;
        }

        public float GetChangetime()
        {
            return _sizeChangeTime.Value;
        }

        public void PauseCharacter()
        {
            if (_isGamePaused)
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
            _isInputBlocked.Value = _isGamePaused;
        }

        public void Hit()
        {
            _currentHitPoints--;
            if (_currentHitPoints == 0)
            {
                _isInputBlocked.Value = true;
                _movementValue = 0.0f;
                _currentHitPoints = _hitPoints.Value;
                _myAnimator.SetTrigger("death");
            }
            else
            {
                _hitEvent.Invoke();
            }
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (!_isInputBlocked.Value)
            {
                PlayerCharacterState newState = _myState.Movement(context.ReadValue<float>());
                if (newState != null)
                {
                    _myState = newState;
                }
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed && !_isInputBlocked.Value)
            {
                PlayerCharacterState newState = _myState.Jump();
                if (newState != null)
                {
                    _myState = newState;
                }
            }
        }

        public void OnResize(InputAction.CallbackContext context)
        {
            if (context.performed && !_isInputBlocked.Value)
            {
                _myState.Resize();
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed && !_isInputBlocked.Value)
            {
                PlayerCharacterState newState = _myState.Attack();
                if (newState != null)
                {
                    _myState = newState;
                }
            }
        }

        public virtual void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed && !_isInputBlocked.Value && Time.time - _lastDashTime > _dashCooldownTime.Value)
            {
                PlayerCharacterState newState = _myState.Dash();
                if (newState != null)
                {
                    _myState = newState;
                }
            }
        }

        public virtual void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed && !_isInputBlocked.Value)
            {
                _myState.Interact();
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _pauseEvent.Invoke();
            }
        }

        public void OnMenu(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _menuEvent.Invoke();
            }
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {

        }

        public bool RightFacing()
        {
            return _isFacingRight;
        }

        public void StartSacrificeFire()
        {
            _sacrificeFire.Play();
            _lampParticleSystem.Stop();
        }

        public void StartAttack()
        {
            _myAttack.MakeAttack(0.0f, null);
        }

        public void BlockInput()
        {
            _isInputBlocked.Value = true;
        }

        public void UnblockInput()
        {
            _isInputBlocked.Value = false;
        }
    }
}
