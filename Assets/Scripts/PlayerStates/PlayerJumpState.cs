using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class PlayerJumpState : PlayerCharacterState
    {
        public PlayerJumpState(PlayerCharacterController controller) : base(controller)
        {
            LayerMask groundMask = _myController._whatIsGround;
            Transform groundCheck = _myController._groundCheck;
            Animator controlledAnimator = _myController._myAnimator;
            if (CheckIfLanded(groundMask, groundCheck.position))
            {
                float sizeModifier = 1.0f;
                if (_myController._isPlayerSmall.Value)
                {
                    sizeModifier = 2 / _myController._sizeChangeFactor.Value;
                }
                _myController._myJump.Jump(_jumpYVelocity * sizeModifier);
            }
            controlledAnimator.SetTrigger("jump");
            controlledAnimator.SetBool("land", false);
            _gizmoColor = Color.yellow;
        }

        public override PlayerCharacterState StateFixedUpdate()
        {
            if (!_myController._isGamePaused)
            {
                _myController._myMovement.Move(_myController._movementValue * _myController._currentMovementSpeed);
            }
            else
            {
                _myController._myMovement.Move(0.0f);
            }
            LayerMask groundMask = _myController._whatIsGround;
            Transform groundCheck = _myController._groundCheck;
            Rigidbody2D controlledBody = _myController._myBody;
            Animator controlledAnimator = _myController._myAnimator;
            controlledAnimator.SetFloat("jump_speed", controlledBody.velocity.y);
            if (CheckIfLanded(groundMask, groundCheck.position) && controlledBody.velocity.y < 0.1f)
            {
                controlledAnimator.ResetTrigger("jump");
                controlledAnimator.SetBool("land", true);
                return new PlayerOnGroundState(_myController);
            }
            else
            {
                return null;
            }
        }

        public override PlayerCharacterState Jump()
        {
            return null;
        }
    }
}
