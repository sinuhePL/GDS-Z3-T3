using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GDS3
{
    [CreateAssetMenu(fileName = "DashingEnemyBrain", menuName = "Scriptable Objects/DashingEnemyBrain")]
    public class DashingEnemyBrain : CharacterBrain
    {
        [SerializeField] [Range(0.0f, 20.0f)] private float _patrolRange = 2.0f;
        public FloatReference _movementVelocity;
        public FloatReference _dashForce;
        [SerializeField] [Range(0.0f, 20.0f)] private float _playerDetectionZoneWidth = 5.0f;
        [SerializeField] [Range(0.0f, 5.0f)] private float _playerDetectionZoneHeight = 3.0f;
        public LayerMask _playerMask;
        private IControllable _controlledCharacter;
        private Transform _controlledTransform;
        private Vector3 _startingPosition;

        public override void Initialize(IControllable character)
        {
            _controlledTransform = character.GetTransform();
            _controlledCharacter = character;
            _startingPosition = _controlledTransform.position;
            if (_controlledTransform.localScale.x > 0)
            {
                _controlledCharacter.MoveMe(_movementVelocity.Value);
            }
            else
            {
                _controlledCharacter.MoveMe(-_movementVelocity.Value);
            }
        }

        public override void ThinkAboutAnimation(float deltaTime)
        {
            
        }

        public override void ThinkAboutPhysics()
        {
            Collider2D[] hitPlayers = Physics2D.OverlapBoxAll(_controlledTransform.position + new Vector3(0.0f, _playerDetectionZoneHeight/2, 0.0f), new Vector2(_playerDetectionZoneWidth, _playerDetectionZoneHeight), 0.0f, _playerMask);
            if (hitPlayers.Length > 0)
            {
                if(hitPlayers[0].gameObject.transform.position.x > _controlledTransform.position.x)
                {
                    _controlledCharacter.ApplyForce(new Vector2(_dashForce.Value, 0.0f));
                }
                else if(hitPlayers[0].gameObject.transform.position.x < _controlledTransform.position.x)
                {
                    _controlledCharacter.ApplyForce(new Vector2(-_dashForce.Value, 0.0f));
                }
            }
            else
            {
                if (_controlledTransform.position.x < _startingPosition.x - _patrolRange)
                {
                    _controlledCharacter.MoveMe(_movementVelocity.Value);
                }
                else if (_controlledTransform.position.x > _startingPosition.x + _patrolRange)
                {
                    _controlledCharacter.MoveMe(-_movementVelocity.Value);
                }
            }
        }

        public override void DrawGizmo()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector3(_startingPosition.x, _startingPosition.y, 0.0f), new Vector3(_patrolRange * 2.0f, 0.05f, 0.0f));
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(new Vector3(_controlledTransform.position.x, _controlledTransform.position.y + _playerDetectionZoneHeight / 2, 0.0f), new Vector3(_playerDetectionZoneWidth, _playerDetectionZoneHeight, 0.0f));
        }
    }
}
