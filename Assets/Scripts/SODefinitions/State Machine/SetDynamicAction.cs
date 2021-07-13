using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "SetDynamicAction", menuName = "Scriptable Objects/AI/Set Dynamic Action")]
    public class SetDynamicAction : Action
    {
        public override void Act(CharacterBrain brain)
        {
            GameCharacterController controlledCharacter = brain._controlledCharacter;
            Rigidbody2D controlledBody = controlledCharacter.GetRigidbody2D();
            controlledBody.isKinematic = false;
        }

        public override void ActFixed(CharacterBrain brain)
        {

        }
    }
}
