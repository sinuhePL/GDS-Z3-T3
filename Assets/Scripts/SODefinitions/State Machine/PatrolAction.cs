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
            Transform characterTransform = controlledCharacter.GetTransform();
            if (characterTransform.position.x < brain._startingPosition.x - brain._patrolRange.Value)
            {
                controlledCharacter.MoveMe(brain._movementSpeed.Value);
            }
            else if (characterTransform.position.x > brain._startingPosition.x + brain._patrolRange.Value)
            {
                controlledCharacter.MoveMe(-brain._movementSpeed.Value);
            }
            else if (characterTransform.localScale.x > 0)
            {
                controlledCharacter.MoveMe(brain._movementSpeed.Value);
            }
            else
            {
                controlledCharacter.MoveMe(-brain._movementSpeed.Value);
            }
        }
    }
}
