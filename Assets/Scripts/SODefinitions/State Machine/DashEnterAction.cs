using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "DashEnterAction", menuName = "Scriptable Objects/AI/Dash Enter Action")]
    public class DashEnterAction : Action
    {
        public override void Act(CharacterBrain brain)
        {
            IControllable controlledCharacter = brain._controlledCharacter;
            Transform controlledTransform = controlledCharacter.GetTransform();
            brain._currentCooldownTime = brain._dashCooldownTime.Value;
            if (controlledTransform.position.x < brain._targetTransform.position.x)
            {
                brain._attackEndPosition = new Vector3(controlledTransform.position.x + brain._dashDistance.Value, controlledTransform.position.y, controlledTransform.position.z);
            }
            else
            {
                brain._attackEndPosition = new Vector3(controlledTransform.position.x - brain._dashDistance.Value, controlledTransform.position.y, controlledTransform.position.z);
            }
        }

        public override void ActFixed(CharacterBrain brain)
        {
            
        }
    }
}
