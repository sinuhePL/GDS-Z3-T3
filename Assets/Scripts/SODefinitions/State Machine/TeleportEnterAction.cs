using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "TeleportEnterAction", menuName = "Scriptable Objects/AI/Teleport Enter Action")]
    public class TeleportEnterAction : Action
    {
        public override void Act(CharacterBrain brain)
        {
            IControllable controlledCharacter = brain._controlledCharacter;
            Transform controlledTransform = controlledCharacter.GetTransform();
            Rigidbody2D controlledBody = controlledCharacter.GetRigidbody2D();
            brain._currentCooldownTime = brain._teleportCooldownTime.Value;
            float distance = Mathf.Abs(controlledTransform.position.x - brain._targetTransform.position.x) + brain._teleportDistance.Value;
            Debug.Log("Wchodzę w teleport");
            if (controlledTransform.position.x < brain._targetTransform.position.x && controlledTransform.position.x + distance < brain._startingPosition.x + brain._rightMaxMoveDistance.Value)
            {
                controlledBody.isKinematic = true;
                controlledBody.transform.position = new Vector3(controlledTransform.position.x + distance, controlledTransform.position.y, controlledTransform.position.z);
                controlledBody.isKinematic = false;
            }
            else if(controlledTransform.position.x > brain._targetTransform.position.x && controlledTransform.position.x - distance > brain._startingPosition.x - brain._leftMaxMoveDistance.Value) 
            {
                controlledBody.isKinematic = true;
                controlledBody.transform.position = new Vector3(controlledTransform.position.x - distance, controlledTransform.position.y, controlledTransform.position.z);
                controlledBody.isKinematic = false;
            }
        }

        public override void ActFixed(CharacterBrain brain)
        {
            
        }
    }
}
