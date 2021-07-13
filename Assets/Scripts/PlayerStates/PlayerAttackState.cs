using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class PlayerAttackState : PlayerCharacterState
    {
        private bool _attackEnd;

        private void AttackEnd()
        {
            Animator controlledAnimator = _myController._myAnimator;
            controlledAnimator.ResetTrigger("attack");
            _attackEnd = true;
        }

        public PlayerAttackState(PlayerCharacterController controller) : base(controller)
        {
            _attackEnd = false;
            _myController._myAnimator.SetTrigger("attack");
            _myController._currentCooldownTime = _myController._attackLength;
            _myController._myAttack.MakeAttack(_myController._attackLength, AttackEnd);
            _gizmoColor = Color.red;
        }

        public override PlayerCharacterState Attack()
        {
            return null;
        }

        public override PlayerCharacterState Dash()
        {
            return null;
        }

        public override PlayerCharacterState StateFixedUpdate()
        {
            LayerMask groundMask = _myController._whatIsGround;
            Transform groundCheck = _myController._groundCheck;
            if (_attackEnd)
            {
                if(CheckIfLanded(groundMask, groundCheck.position))
                {
                    return new PlayerOnGroundState(_myController);
                }
                else
                {
                    return new PlayerJumpState(_myController);
                }
            }
            else
            {
                return null;
            }
        }
    }
}
