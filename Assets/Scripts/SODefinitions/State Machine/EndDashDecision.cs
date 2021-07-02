using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "EndDashDecision", menuName = "Scriptable Objects/AI/End Dash Decision")]
    public class EndDashDecision : Decision
    {
        public override bool Decide(CharacterBrain brain)
        {
            IControllable controlledCharacter = brain._controlledCharacter;
            Transform controlledTransform = controlledCharacter.GetTransform();
            if(Mathf.Abs(controlledTransform.position.x - brain._attackEndPosition.x) < 0.2f)
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
