using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "LandDecision", menuName = "Scriptable Objects/AI/Land Decision")]
    public class LandDecision : Decision
    {
        private bool CheckIfLanded(LayerMask mask, Vector3 checkPosition)
        {
            const float groundCheckRadius = 0.2f;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(checkPosition, groundCheckRadius, mask);
            foreach (Collider2D collider in colliders)
            {
                return true;
            }
            return false;
        }

        public override bool Decide(CharacterBrain brain)
        {
            LayerMask groundMask = brain._controlledCharacter.GetGroundMask();
            Transform groundCheck = brain._controlledCharacter.GetGroundCheck();
            Rigidbody2D controlledBody = brain._controlledCharacter.GetRigidbody2D();
            bool isLanded = CheckIfLanded(groundMask, groundCheck.position);
            return isLanded;       
        }
    }
}
