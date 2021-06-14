using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "RestDecision", menuName = "Scriptable Objects/AI/Rest Decision")]
    public class RestDecision : Decision
    {
        public override bool Decide(CharacterBrain brain)
        {
            if(brain._stateTimeElapsted > brain._cooldownTime.Value)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
