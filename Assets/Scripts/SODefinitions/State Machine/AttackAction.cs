using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "AttackAction", menuName = "Scriptable Objects/AI/Attack Action")]
    public class AttackAction : Action
    {
        public override void Act(CharacterBrain brain)
        {

        }

        public override void ActFixed(CharacterBrain brain)
        {
            brain._controlledCharacter.MoveMe(brain._movementValue * brain._currentMovementSpeed);
        }
    }
}
