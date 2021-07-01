using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "PlayerAttackDecision", menuName = "Scriptable Objects/AI/Player Attack Decision")]
    public class PlayerAttackDecision : Decision
    {
        public override bool Decide(CharacterBrain brain)
        {
            if (brain._attackPressed)
            {
                brain._attackPressed = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
