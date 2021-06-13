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
            if(Input.GetButtonDown("Fire1"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
