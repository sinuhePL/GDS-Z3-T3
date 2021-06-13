using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "DashAction", menuName = "Scriptable Objects/AI/Dash Action")]
    public class DashAction : Action
    {
        public override void Act(CharacterBrain brain)
        {
            
        }

        public override void ActFixed(CharacterBrain brain)
        {
            Transform target = brain._targetTransform;
            IControllable controlledCharacter = brain._controlledCharacter;
            Transform controlledTransform = controlledCharacter.GetTransform();
            if (target.position.x > controlledTransform.position.x)
            {
                controlledCharacter.ApplyForce(new Vector2(brain._dashForce.Value, 0.0f));
            }
            else if (target.position.x < controlledTransform.position.x)
            {
                controlledCharacter.ApplyForce(new Vector2(-brain._dashForce.Value, 0.0f));
            }
        }
    }
}
