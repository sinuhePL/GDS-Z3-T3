using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "RestEnterAction", menuName = "Scriptable Objects/AI/Rest Enter Action")]
    public class RestEnterAction : Action
    {
        public override void Act(CharacterBrain brain)
        {
            GameCharacterController controlledCharacter = brain._controlledCharacter;
            Animator controlledAnimator = controlledCharacter.GetAnimator();
            controlledAnimator.SetTrigger("cooldown");
        }

        public override void ActFixed(CharacterBrain brain)
        {

        }
    }
}
