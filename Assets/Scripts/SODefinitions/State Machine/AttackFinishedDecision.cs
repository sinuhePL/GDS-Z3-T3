using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "AttackFinishedDecision", menuName = "Scriptable Objects/AI/Attack Finished Decision")]
    public class AttackFinishedDecision : Decision
    {
        public override bool Decide(CharacterBrain brain)
        {
            if (brain._isAttackFinished)
            {
                brain._isAttackFinished = false;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
