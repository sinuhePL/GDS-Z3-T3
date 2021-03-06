using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "newBrain", menuName = "Scriptable Objects/AI/Brain")]
    public class CharacterBrain : ScriptableObject
    {
        [Header("Movement Ability")]
        public FloatReference _movementSpeed;
        public FloatReference _leftMaxMoveDistance;
        public FloatReference _rightMaxMoveDistance;
        [Header("Jump Ability")]
        public FloatReference _jumpTime;
        public FloatReference _jumpHeight;
        public FloatReference _jumpHorizontalSpeed;
        [Header("Dash Ability")]
        public FloatReference _dashVelocity;
        public FloatReference _dashDistance;
        public FloatReference _dashCooldownTime;
        public LayerMask _dashObstacles;
        [Header("Patrol Ability")]
        public FloatReference _patrolRange;
        public FloatReference _playerDetectionZoneWidth;
        public FloatReference _playerDetectionZoneHeight;
        public LayerMask _targetMask;
        public BoolReference _isPlayerSmall;
        [Header("Teleport Ability")]
        public FloatReference _teleportDistance;
        public FloatReference _teleportCooldownTime;
        [Header("Other")]
        public IntegerReference _hitPoints;
        public IntegerReference _currentHitPoints;
        [HideInInspector] public State _currentState;
        [HideInInspector] public GameCharacterController _controlledCharacter;
        [HideInInspector] public Vector3 _startingPosition;
        [HideInInspector] public Transform _targetTransform;
        [HideInInspector] public Vector3 _attackEndPosition;
        [HideInInspector] public float _jumpYVelocity;
        [HideInInspector] public float _stateTimeElapsted;
        [HideInInspector] public float _currentCooldownTime;
        [HideInInspector] public bool _isAttackFinished;

        private const float _gizmoThickness = 0.05f;
        private const float _gizmoHeight = 3.0f;

        public void Initialize(GameCharacterController character)
        {
            _controlledCharacter = character;
            Transform characterTransform = character.GetTransform();
            _startingPosition = characterTransform.position;
            if (_jumpHeight.Value > 0.0f && _jumpTime.Value > 0.0f)
            {
                float gForce = ((2 * _jumpHeight.Value) / (_jumpTime.Value * _jumpTime.Value)) / 9.81f;
                Rigidbody2D rigidbody = character.GetRigidbody2D();
                rigidbody.gravityScale = gForce;
                _jumpYVelocity = (2 * _jumpHeight.Value) / _jumpTime.Value;
            }
            _currentState = _controlledCharacter.GetStartingState();
            _currentState.OnEnterState(this);
            _isAttackFinished = false;
            _currentHitPoints.Value = _hitPoints.Value;
        }

        public void ThinkAboutAnimation()
        {
            _currentState.UpdateState(this);
            _stateTimeElapsted += Time.deltaTime;
        }

        public void ThinkAboutPhysics()
        {
            _currentState.FixedUpdateState(this);
        }

        public void DrawGizmo(Vector3 drawerPosition)
        {
            if(_leftMaxMoveDistance.Value > 0.0f)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(new Vector3(_startingPosition.x - _leftMaxMoveDistance.Value, _startingPosition.y + _gizmoHeight/2, 0.0f), new Vector3(_gizmoThickness, _gizmoHeight, 0.0f));
            }
            if(_rightMaxMoveDistance.Value > 0.0f)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(new Vector3(_startingPosition.x + _rightMaxMoveDistance.Value, _startingPosition.y + _gizmoHeight/2, 0.0f), new Vector3(_gizmoThickness, _gizmoHeight, 0.0f));
            }
            if (_patrolRange.Value > 0.0f)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(new Vector3(_startingPosition.x, _startingPosition.y, 0.0f), new Vector3(_patrolRange.Value * 2.0f, _gizmoThickness, 0.0f));
            }
            if (_playerDetectionZoneWidth.Value > 0.0f && _playerDetectionZoneHeight.Value > 0.0f)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(new Vector3(drawerPosition.x, drawerPosition.y + _playerDetectionZoneHeight.Value / 2, 0.0f), new Vector3(_playerDetectionZoneWidth.Value, _playerDetectionZoneHeight.Value, 0.0f));
            }
            if(_jumpHeight.Value > 0.0f)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(new Vector3(drawerPosition.x, drawerPosition.y + _jumpHeight.Value*0.95f, 0.0f), new Vector3(_gizmoHeight, _gizmoThickness, 0.0f));
            }
            if(_currentState != null)
            {
                Gizmos.color = _currentState._stateGizmoColor;
                Gizmos.DrawWireSphere(drawerPosition, 0.5f);
            }
        }

        public void TransitionToState(State newState)
        {
            _currentState = newState;
            _stateTimeElapsted = 0.0f;
            _currentState.OnEnterState(this);
        }
    }
}
