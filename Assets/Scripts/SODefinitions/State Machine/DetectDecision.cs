using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    [CreateAssetMenu(fileName = "DetectDecision", menuName = "Scriptable Objects/AI/Detect Decision")]
    public class DetectDecision : Decision
    {
        private bool CheckIfTargetNearby(CharacterBrain brain)
        {
            Transform checkTransform = brain._controlledCharacter.GetTransform();
            float detectZoneHeight = brain._playerDetectionZoneHeight.Value;
            float detectZoneWidth = brain._playerDetectionZoneWidth.Value;
            LayerMask targetMask = brain._targetMask;
            Collider2D[] hitPlayers = Physics2D.OverlapBoxAll(checkTransform.position + new Vector3(0.0f, detectZoneHeight / 2, 0.0f), new Vector2(detectZoneWidth, detectZoneHeight), 0.0f, targetMask);
            if(hitPlayers.Length > 0)
            {
                Transform hitTransform = hitPlayers[0].gameObject.transform;
                if (hitTransform.position.x < checkTransform.position.x && hitTransform.position.x < brain._startingPosition.x - brain._leftMaxMoveDistance.Value ||
                    hitTransform.position.x > checkTransform.position.x && hitTransform.position.x > brain._startingPosition.x + brain._rightMaxMoveDistance.Value ||
                    brain._isPlayerSmall.Value)
                {
                    return false;
                }
                else
                {
                    brain._targetTransform = hitTransform;
                    return true;
                }
            }
            else
            {
                brain._targetTransform = null;
                return false;
            }
        }

        public override bool Decide(CharacterBrain brain)
        {
            bool isDetected = CheckIfTargetNearby(brain);
            return isDetected;
        }
    }
}
