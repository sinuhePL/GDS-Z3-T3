using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GDS3
{
    [CreateAssetMenu(fileName = "DashingEnemyBrain", menuName = "Scriptable Objects/DashingEnemyBrain")]
    public class DashingEnemyBrain : CharacterBrain
    {
        public FloatReference _patrolRange;
        public FloatReference _movementVelocity;
        public FloatReference _dashVelocity;
        public FloatReference _attackRange;
        public LayerMask _playerMask;
        private IControllable _controlledCharacter;
        private Transform _controlledTransform;
        private Vector3 _startingPosition;

        public override void Initialize(IControllable character)
        {
            _controlledTransform = character.GetTransform();
            _controlledCharacter = character;
            _startingPosition = _controlledTransform.position;
            if(_controlledTransform.localScale.x > 0)
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
            Collider2D[] hitPlayers = Physics2D.OverlapBoxAll(_controlledTransform.position, new Vector2(_attackRange.Value * 2, 3.0f), 0.0f, _playerMask);
            if (hitPlayers.Length > 0)
            {
                if(hitPlayers[0].gameObject.transform.position.x > _controlledTransform.position.x)
                {
                    _controlledCharacter.ApplyForce(new Vector2(_dashVelocity.Value, 0.0f));
                }
                else if(hitPlayers[0].gameObject.transform.position.x < _controlledTransform.position.x)
                {
                    _controlledCharacter.ApplyForce(new Vector2(-_dashVelocity.Value, 0.0f));
                }
            }
            else
            {
                if (_controlledTransform.position.x < _startingPosition.x - _patrolRange.Value)
                {
                    _controlledCharacter.MoveMe(_movementVelocity.Value);
                }
                else if (_controlledTransform.position.x > _startingPosition.x + _patrolRange.Value)
                {
                    _controlledCharacter.MoveMe(-_movementVelocity.Value);
                }
            }
        }
    }
}
