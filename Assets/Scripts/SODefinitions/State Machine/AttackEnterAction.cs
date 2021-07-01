using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "Attack Enter Action", menuName = "Scriptable Objects/AI/Attack Enter Action")]
    public class AttackEnterAction : Action
    {
        public override void Act(CharacterBrain brain)
        {
            IControllable controlledCharacter = brain._controlledCharacter;
            controlledCharacter.Attack();
        }

        public override void ActFixed(CharacterBrain brain)
        {
        }
    }
}
