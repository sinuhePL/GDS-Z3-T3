using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "PlayerControlledBrain", menuName = "Scriptable Objects/PlayerControlledBrain")]
    public class PlayerControlledBrain : CharacterBrain
    {
        public FloatReference _maxMovementVelocity;
        public FloatReference _jumpTime;
        public FloatReference _jumpHeight;
        public BoolReference _isSmallSize;
        public FloatReference _sizeChangeFactor;
        private IControllable _controlledCharacter;
        private float _horizontalMove;
        private bool _jumpPressed = false;
        private float _jumpYVelocity;

        public override void Initialize(IControllable character)
        {
            float gForce = ((2 * _jumpHeight.Value) / (_jumpTime.Value * _jumpTime.Value))/9.81f;
            Rigidbody2D rigidbody = character.GetRigidbody2D();
            rigidbody.gravityScale = gForce;
            _jumpYVelocity = (2 * _jumpHeight.Value)/_jumpTime.Value;
            _controlledCharacter = character;
        }

        public override void ThinkAboutAnimation(float deltaTime)
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal");
            if (!_jumpPressed && Input.GetButtonDown("Fire1"))
            {
                _jumpPressed = true;
            }
        }

        public override void ThinkAboutPhysics()
        {
            float sizeModifier = 1.0f;
            if(_isSmallSize.Value)
            {
                sizeModifier = 2 / _sizeChangeFactor.Value;
            }
            _controlledCharacter.MoveMe(_horizontalMove * _maxMovementVelocity.Value);
            if(_jumpPressed)
            {
                _controlledCharacter.Jump(_jumpYVelocity * sizeModifier);
                _jumpPressed = false;
            }
        }

        public override void DrawGizmo()
        {

        }
    }
}
