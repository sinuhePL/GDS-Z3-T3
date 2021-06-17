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
            IControllable controlledCharacter = brain._controlledCharacter;
            Transform characterTransform = controlledCharacter.GetTransform();
            if (characterTransform.position.x < brain._startingPosition.x - brain._patrolRange.Value)
            {
                controlledCharacter.MoveMe(brain._currentMovementSpeed);
            }
            else if (characterTransform.position.x > brain._startingPosition.x + brain._patrolRange.Value)
            {
                controlledCharacter.MoveMe(-brain._currentMovementSpeed);
            }
        }
    }
}
