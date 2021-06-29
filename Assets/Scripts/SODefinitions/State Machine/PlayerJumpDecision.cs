using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "PlayerJumpDecision", menuName = "Scriptable Objects/AI/Player Jump Decision")]
    public class PlayerJumpDecision : Decision
    {
        public override bool Decide(CharacterBrain brain)
        {
            if(brain._jumpPressed)
            {
                brain._jumpPressed = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
