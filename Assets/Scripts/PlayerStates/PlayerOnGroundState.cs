using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GDS3
{
    public class PlayerOnGroundState : PlayerCharacterState
    {
        public PlayerOnGroundState(PlayerCharacterController controller) : base(controller)
        {

        }

        public override PlayerCharacterState Dash()
        {
            return null;
        }
    }
}
