using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "JumpEnterAction", menuName = "Scriptable Objects/AI/Jump Enter Action")]
    public class JumpEnterAction : Action
    {
        public override void Act(CharacterBrain brain)
        {
           
        }

        public override void ActFixed(CharacterBrain brain)
        {
            float sizeModifier = 1.0f;
            if (brain._isCharacterSmall.Value)
            {
                sizeModifier = 2 / brain._sizeChangeFactor.Value;
            }
            brain._controlledCharacter.Jump(brain._jumpYVelocity * sizeModifier);
        }
    }
}
