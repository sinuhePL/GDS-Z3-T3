using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "EscapeAction", menuName = "Scriptable Objects/AI/Escape Action")]
    public class EscapeAction : Action
    {
        public override void Act(CharacterBrain brain)
        {

        }

        public override void ActFixed(CharacterBrain brain)
        {
            GameCharacterController controlledCharacter = brain._controlledCharacter;
            Transform characterTransform = controlledCharacter.GetTransform();
            if (characterTransform.position.x < brain._targetTransform.position.x)
            {
                controlledCharacter.MoveMe(-brain._jumpHorizontalSpeed.Value);
            }
            else
            {
                controlledCharacter.MoveMe(brain._jumpHorizontalSpeed.Value);
            }
        }
    }
}
