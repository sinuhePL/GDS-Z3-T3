using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GDS3
{
    [CreateAssetMenu(fileName = "PlayerMoveAction", menuName = "Scriptable Objects/AI/Player Move Action")]
    public class PlayerMoveAction : Action
    {
        private bool CheckIfResizeBlocked(Vector3 checkPosition, float checkDistance, LayerMask resizeBlockerMask)
        {
            RaycastHit2D[] blokers = Physics2D.RaycastAll(checkPosition, Vector3.up, checkDistance, resizeBlockerMask);
            if(blokers.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Act(CharacterBrain brain)
        {
            IControllable controlledCharacter = brain._controlledCharacter;
            if(brain._resizePressed)
            {
                brain._resizePressed = false;
                if (brain._isCharacterSmall.Value) 
                {
                    float distance = (controlledCharacter.GetHeightCheck().position.y - controlledCharacter.GetGroundCheck().position.y) * brain._sizeChangeFactor.Value;
                    if (!CheckIfResizeBlocked(controlledCharacter.GetHeightCheck().position, distance, controlledCharacter.GetResizeBlockerMask()))
                    {
                        brain._isCharacterSmall.Value = false;
                        brain._sizeChangeEvent.Invoke();
                    }
                }
                else
                {
                    brain._isCharacterSmall.Value = true;
                    brain._sizeChangeEvent.Invoke();
                }
            }
            if(brain._interactPressed)
            {
                brain._interactPressed = false;
                float interactionRange;
                if(brain._isPlayerSmall.Value)
                {
                    interactionRange = brain._interactionRange.Value / brain._sizeChangeFactor.Value;
                }
                else
                {
                    interactionRange = brain._interactionRange.Value;
                }
                Transform interactionTransform = controlledCharacter.GetInteractionCheck();
                Collider2D[] colliders = Physics2D.OverlapCircleAll(interactionTransform.position, interactionRange, brain._interactionMask);
                foreach(Collider2D collider in colliders)
                {
                    Interactable interactableObject = collider.gameObject.GetComponent<Interactable>();
                    if(interactableObject != null)
                    {
                        interactableObject.Interact(controlledCharacter.GetHandTransform(), 0.5f);
                    }
                }
            }
        }

        public override void ActFixed(CharacterBrain brain)
        {
            brain._controlledCharacter.MoveMe(brain._movementValue * brain._currentMovementSpeed);
        }
    }
}
