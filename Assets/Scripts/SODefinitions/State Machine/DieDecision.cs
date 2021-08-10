using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "DieDecision", menuName = "Scriptable Objects/AI/Die Decision")]
    public class DieDecision : Decision
    {
        public override bool Decide(CharacterBrain brain)
        {
            if (brain._currentHitPoints < 1)
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
