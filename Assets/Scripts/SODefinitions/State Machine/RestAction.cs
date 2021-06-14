using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "RestAction", menuName = "Scriptable Objects/AI/Rest Action")]
    public class RestAction : Action
    {
        public override void Act(CharacterBrain brain)
        {

        }

        public override void ActFixed(CharacterBrain brain)
        {
            IControllable controlledCharacter = brain._controlledCharacter;
            Rigidbody2D characterBody = controlledCharacter.GetRigidbody2D();
            float speed = characterBody.velocity.x;
            speed *= 0.9f;
            if(speed < 0.1f)
            {
                speed = 0.0f;
            }
            controlledCharacter.MoveMe(speed);
        }
    }
}
