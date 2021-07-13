using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "PatrolEnterAction", menuName = "Scriptable Objects/AI/Patrol Enter Action")]
    public class PatrolEnterAction : Action
    {
        public override void Act(CharacterBrain brain)
        {
            
        }

        public override void ActFixed(CharacterBrain brain)
        {
            GameCharacterController controlledCharacter = brain._controlledCharacter;
            Transform controlledTransform = controlledCharacter.GetTransform();
            Rigidbody2D controlledBody = controlledCharacter.GetRigidbody2D();
            controlledBody.isKinematic = true;
            controlledBody.velocity = Vector3.zero;
            Debug.Log("Wchodzę w patrol");
            if (controlledTransform.localScale.x > 0)
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