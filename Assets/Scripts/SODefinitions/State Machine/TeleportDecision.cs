using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "TeleportDecision", menuName = "Scriptable Objects/AI/Teleport Decision")]
    public class TeleportDecision : Decision
    {
        public override bool Decide(CharacterBrain brain)
        {
            IControllable controlledCharacter = brain._controlledCharacter;
            Transform controlledTransform = controlledCharacter.GetTransform();
            float distance = Mathf.Abs(controlledTransform.position.x - brain._targetTransform.position.x) + brain._teleportDistance.Value;
            if (brain._stateTimeElapsted > brain._currentCooldownTime)
            {
                if (controlledTransform.position.x < brain._targetTransform.position.x && controlledTransform.position.x + distance < brain._startingPosition.x + brain._rightMaxMoveDistance.Value ||
                    controlledTransform.position.x > brain._targetTransform.position.x && controlledTransform.position.x - distance > brain._startingPosition.x - brain._leftMaxMoveDistance.Value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
