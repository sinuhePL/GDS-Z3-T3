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
            GameCharacterController controlledCharacter = brain._controlledCharacter;
            Transform controlledTransform = controlledCharacter.GetTransform();
            if (brain._attackEndPosition.x > controlledTransform.position.x)
            {
                controlledCharacter.MoveMe(brain._dashVelocity.Value);
            }
            else if (brain._attackEndPosition.x < controlledTransform.position.x)
            {
                controlledCharacter.MoveMe(-brain._dashVelocity.Value);
            }
        }
    }
}
