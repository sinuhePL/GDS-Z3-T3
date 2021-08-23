using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "TeleportEnterAction", menuName = "Scriptable Objects/AI/Teleport Enter Action")]
    public class TeleportEnterAction : Action
    {
        private IEnumerator DelayTeleport(float seconds, CharacterBrain myBrain, Vector3 newPosition)
        {
            GameCharacterController controlledCharacter = myBrain._controlledCharacter;
            Rigidbody2D controlledBody = controlledCharacter.GetRigidbody2D();
            Animator controlledAnimator = controlledCharacter.GetAnimator();
            yield return new WaitForSeconds(seconds);
            if (myBrain._currentHitPoints > 0)
            {
                controlledBody.isKinematic = true;
                controlledBody.transform.position = newPosition;
                controlledBody.isKinematic = false;
                controlledAnimator.SetTrigger("teleportEnd");
            }
        }

        public override void Act(CharacterBrain brain)
        {
            GameCharacterController controlledCharacter = brain._controlledCharacter;
            Transform controlledTransform = controlledCharacter.GetTransform();
            Rigidbody2D controlledBody = controlledCharacter.GetRigidbody2D();
            Animator controlledAnimator = controlledCharacter.GetAnimator();
            brain._currentCooldownTime = brain._teleportCooldownTime.Value;
            float distance = Mathf.Abs(controlledTransform.position.x - brain._targetTransform.position.x) + brain._teleportDistance.Value;
            controlledAnimator.SetTrigger("teleport");
            if (controlledTransform.position.x < brain._targetTransform.position.x && controlledTransform.position.x + distance < brain._startingPosition.x + brain._rightMaxMoveDistance.Value)
            {
                controlledCharacter.StartCoroutine(DelayTeleport(1.16f, brain, new Vector3(controlledTransform.position.x + distance, controlledTransform.position.y, controlledTransform.position.z)));
            }
            else if(controlledTransform.position.x > brain._targetTransform.position.x && controlledTransform.position.x - distance > brain._startingPosition.x - brain._leftMaxMoveDistance.Value) 
            {
                controlledCharacter.StartCoroutine(DelayTeleport(1.16f, brain, new Vector3(controlledTransform.position.x - distance, controlledTransform.position.y, controlledTransform.position.z)));
            }
        }

        public override void ActFixed(CharacterBrain brain)
        {
            
        }
    }
}
