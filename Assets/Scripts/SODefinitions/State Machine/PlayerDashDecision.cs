﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "PlayerDashDecision", menuName = "Scriptable Objects/AI/Player Dash Decision")]
    public class PlayerDashDecision : Decision
    {
        public override bool Decide(CharacterBrain brain)
        {
            if (brain._dashValue > 0.1f || brain._dashValue < -0.1f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}