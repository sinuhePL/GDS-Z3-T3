using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "CooldownEndDecision", menuName = "Scriptable Objects/AI/Cooldown End Decision")]
    public class CooldownEndDecision : Decision
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
