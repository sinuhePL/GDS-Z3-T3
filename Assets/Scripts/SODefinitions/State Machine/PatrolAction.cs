using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "PatrolAction", menuName = "Scriptable Objects/AI/Patrol Action")]
    public class PatrolAction : Action
    {
        public override void Act(CharacterBrain brain)
        {
            
        }

        public override void ActFixed(CharacterBrain brain)
        {
            GameCharacterController controlledCharacter = brain._controlledCharacter;
            Transform controlledTransform = controlledCharacter.GetTransform();
            if (controlledTransform.position.x < brain._startingPosition.x - brain._patrolRange.Value)
            {
                controlledCharacter.MoveMe(brain._movementSpeed.Value, false);
            }
            else if (controlledTransform.position.x > brain._startingPosition.x + brain._patrolRange.Value)
            {
                controlledCharacter.MoveMe(-brain._movementSpeed.Value, false);
            }
            else if (controlledTransform.localScale.x > 0 && controlledCharacter.RightFacing() || controlledTransform.localScale.x < 0 && !controlledCharacter.RightFacing())
            {
                controlledCharacter.MoveMe(brain._movementSpeed.Value, false);
            }
            else
            {
                controlledCharacter.MoveMe(-brain._movementSpeed.Value, false);
            }
        }
    }
}
