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
            Rigidbody2D controlledBody = controlledCharacter.GetRigidbody2D();
            Transform characterTransform = controlledCharacter.GetTransform();
            if (controlledBody.transform.position.x < brain._startingPosition.x + brain._rightMaxMoveDistance.Value && controlledBody.transform.position.x > brain._startingPosition.x - brain._rightMaxMoveDistance.Value)
            {
                if (characterTransform.position.x < brain._targetTransform.position.x)
                {
                    controlledCharacter.MoveMe(-brain._jumpHorizontalSpeed.Value, true);
                }
                else
                {
                    controlledCharacter.MoveMe(brain._jumpHorizontalSpeed.Value, true);
                }
            }
        }
    }
}
