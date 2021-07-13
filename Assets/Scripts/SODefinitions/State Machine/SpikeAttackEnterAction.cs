using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "SpikesEnterAction", menuName = "Scriptable Objects/AI/Spikes Enter Action")]
    public class SpikeAttackEnterAction : Action
    {
        public override void Act(CharacterBrain brain)
        {
        }

        public override void ActFixed(CharacterBrain brain)
        {
            GameCharacterController controlledCharacter = brain._controlledCharacter;
            Transform controlledTransform = controlledCharacter.GetTransform();
            if(brain._targetTransform.position.x > controlledTransform.position.x)
            {
                controlledCharacter.MoveMe(0.01f);
            }
            else
            {
                controlledCharacter.MoveMe(-0.01f);
            }
            controlledCharacter.Attack();
        }
    }
}
