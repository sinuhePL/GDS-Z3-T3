﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "PlayerControlledBrain", menuName = "Scriptable Objects/PlayerControlledBrain")]
    public class PlayerControlledBrain : CharacterBrain
    {
        public FloatReference _maxMovementVelocity;
        public FloatReference _jumpForce;
        public BoolReference _isSmallSize;
        public FloatReference _sizeChangeFactor;
        private GameCharacterController _controlledCharacter;
        private float _horizontalMove;
        private bool _jumpPressed = false;

        public override void Initialize(GameCharacterController character)
        {
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
                _controlledCharacter.JumpMe(_jumpForce.Value * sizeModifier);
                _jumpPressed = false;
            }
        }
    }
}
