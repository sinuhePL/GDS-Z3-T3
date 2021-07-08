using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "PlayerDashEnterAction", menuName = "Scriptable Objects/AI/Player Dash Enter Action")]
    public class PlayerDashEnterAction : Action
    {
        public override void Act(CharacterBrain brain)
        {
            IControllable controlledCharacter = brain._controlledCharacter;
            Transform controlledTransform = controlledCharacter.GetTransform();
            Rigidbody2D controlledBody = controlledCharacter.GetRigidbody2D();
            controlledBody.isKinematic = true;
            controlledBody.velocity = new Vector2(controlledBody.velocity.x, 0.0f);
            brain._currentCooldownTime = brain._dashCooldownTime.Value;
            controlledCharacter.Dash();
            if (controlledTransform.localScale.x < 0.0f)
            {
                brain._attackEndPosition = new Vector3(controlledTransform.position.x + brain._dashDistance.Value, controlledTransform.position.y, controlledTransform.position.z);
            }
            else
            {
                brain._attackEndPosition = new Vector3(controlledTransform.position.x - brain._dashDistance.Value, controlledTransform.position.y, controlledTransform.position.z);
            }
        }

        public override void ActFixed(CharacterBrain brain)
        {

        }
    }
}
