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
                if (Time.time - brain._lastDashTime > brain._dashCooldownTime.Value)
                {
                    brain._lastDashTime = Time.time;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
