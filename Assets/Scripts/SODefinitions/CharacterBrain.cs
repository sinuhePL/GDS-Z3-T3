using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "newBrain", menuName = "Scriptable Objects/AI/Brain")]
    public class CharacterBrain : ScriptableObject
    {
        public State _currentState;
        public FloatReference _movementSpeed;
        public FloatReference _jumpTime;
        public FloatReference _jumpHeight;
        public BoolReference _isPlayerSmall;
        public FloatReference _playerSizeChangeFactor;
        public LayerMask _targetMask;
        public FloatReference _dashForce;
        public FloatReference _patrolRange;
        public FloatReference _playerDetectionZoneWidth;
        public FloatReference _playerDetectionZoneHeight;
        [HideInInspector] public IControllable _controlledCharacter;
        [HideInInspector] public Vector3 _startingPosition;
        [HideInInspector] public Transform _targetTransform;
        [HideInInspector] public float _jumpYVelocity;

        public void Initialize(IControllable character)
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
            _currentState.OnEnterState(this);
        }

        public void ThinkAboutAnimation(float deltaTime)
        {
            _currentState.UpdateState(this);
        }

        public void ThinkAboutPhysics()
        {
            _currentState.FixedUpdateState(this);
        }

        public void DrawGizmo(Vector3 drawerPosition)
        {
            if (_controlledCharacter != null)
            {
                if (_patrolRange.Value > 0.0f)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(new Vector3(_startingPosition.x, _startingPosition.y, 0.0f), new Vector3(_patrolRange.Value * 2.0f, 0.05f, 0.0f));
                }
                if (_playerDetectionZoneWidth.Value > 0.0f && _playerDetectionZoneHeight.Value > 0.0f)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireCube(new Vector3(drawerPosition.x, drawerPosition.y + _playerDetectionZoneHeight.Value / 2, 0.0f), new Vector3(_playerDetectionZoneWidth.Value, _playerDetectionZoneHeight.Value, 0.0f));
                }
                if(_currentState != null)
                {
                    Gizmos.color = _currentState._stateGizmoColor;
                    Gizmos.DrawWireSphere(drawerPosition, 0.5f);
                }
            }
        }

        public void TransitionToState(State newState)
        {
            _currentState = newState;
            _currentState.OnEnterState(this);
        }
    }
}
