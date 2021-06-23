using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "HitDecision", menuName = "Scriptable Objects/AI/Hit Decision")]
    public class HitDecision : Decision
    {
        public override bool Decide(CharacterBrain brain)
        {
            if (brain._isAttackSuccessful)
            {
                brain._isAttackSuccessful = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
