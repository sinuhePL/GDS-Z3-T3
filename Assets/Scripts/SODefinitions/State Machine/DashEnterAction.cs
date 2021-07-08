using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "DashEnterAction", menuName = "Scriptable Objects/AI/Dash Enter Action")]
    public class DashEnterAction : Action
    {
        public override void Act(CharacterBrain brain)
        {
            Vector3 dashEndPosition;
            IControllable controlledCharacter = brain._controlledCharacter;
            Transform controlledTransform = controlledCharacter.GetTransform();
            Debug.Log("Wchodzę w  dash");
            controlledCharacter.Attack();
            brain._currentCooldownTime = brain._dashCooldownTime.Value;
            if (controlledTransform.position.x < brain._targetTransform.position.x)
            {
                dashEndPosition = new Vector3(controlledTransform.position.x + brain._dashDistance.Value, controlledTransform.position.y, controlledTransform.position.z);
                if (dashEndPosition.x > brain._startingPosition.x + brain._rightMaxMoveDistance.Value)
                {
                    dashEndPosition.x = brain._startingPosition.x + brain._rightMaxMoveDistance.Value;
                }
                brain._attackEndPosition = dashEndPosition;
            }
            else
            {
                dashEndPosition = new Vector3(controlledTransform.position.x - brain._dashDistance.Value, controlledTransform.position.y, controlledTransform.position.z);
                if(dashEndPosition.x < brain._startingPosition.x - brain._leftMaxMoveDistance.Value)
                {
                    dashEndPosition.x = brain._startingPosition.x - brain._leftMaxMoveDistance.Value;
                }
                brain._attackEndPosition = dashEndPosition;
            }
        }

        public override void ActFixed(CharacterBrain brain)
        {
            
        }
    }
}
