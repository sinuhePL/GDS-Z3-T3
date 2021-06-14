using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "HitDecision", menuName = "Scriptable Objects/AI/Hit Decision")]
    public class HitDecision : Decision
    {
        private bool CheckIfTargetHit(CharacterBrain brain)
        {
            Transform checkTransform = brain._controlledCharacter.GetHitCheck();
            float detectRadius = brain._hitRange.Value;
            LayerMask targetMask = brain._targetMask;
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(checkTransform.position, detectRadius, targetMask);
            if (hitPlayers.Length > 0)
            {
                if(targetMask == LayerMask.GetMask("Player"))
                {
                    brain._playerKilledEvent.Invoke();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool Decide(CharacterBrain brain)
        {
            bool isHit = CheckIfTargetHit(brain);
            return isHit;
        }
    }
}
