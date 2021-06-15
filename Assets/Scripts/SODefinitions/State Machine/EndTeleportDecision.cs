using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "EndTeleportDecision", menuName = "Scriptable Objects/AI/End Teleport Decision")]
    public class EndTeleportDecision : Decision
    {
        public override bool Decide(CharacterBrain brain)
        {

            if (brain._stateTimeElapsted > brain._currentCooldownTime)
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
