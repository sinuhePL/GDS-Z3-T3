using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "PlayerDashDecision", menuName = "Scriptable Objects/AI/Player Dash Decision")]
    public class PlayerDashDecision : Decision
    {
        public override bool Decide(CharacterBrain brain)
        {
            if (brain._dashPressed)
            {
                brain._dashPressed = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
