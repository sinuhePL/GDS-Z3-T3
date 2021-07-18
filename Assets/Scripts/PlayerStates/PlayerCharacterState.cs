﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace GDS3
{
    public abstract class PlayerCharacterState 
    {
        protected PlayerCharacterController _myController;
        protected float _jumpYVelocity;
        protected Color _gizmoColor;

        private bool CheckIfResizeBlocked(Vector3 checkPosition, float checkDistance, LayerMask resizeBlockerMask)
        {
            RaycastHit2D[] blokers = Physics2D.RaycastAll(checkPosition, Vector3.up, checkDistance, resizeBlockerMask);
            if (blokers.Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool CheckIfLanded(LayerMask mask, Vector3 checkPosition)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(checkPosition, _myController._groundCheckRadius, mask);
            foreach (Collider2D collider in colliders)
            {
                return true;
            }
            return false;
        }

        protected PlayerCharacterState(PlayerCharacterController controller)
        {
            _myController = controller;
            _jumpYVelocity = (2 * _myController._jumpHeight.Value) / _myController._jumpTime.Value;
            _gizmoColor = Color.green;
        }

        public virtual PlayerCharacterState Movement(float movementValue)
        {
            _myController._movementValue = movementValue;
            return null;
        }

        public virtual PlayerCharacterState Jump()
        {
            return new PlayerJumpState(_myController);
        }

        public virtual void Resize()
        {
            if (_myController._isPlayerSmall.Value)
            {
                float distance = (_myController._heightCheck.position.y - _myController._groundCheck.position.y) * _myController._sizeChangeFactor.Value;
                if (!CheckIfResizeBlocked(_myController._heightCheck.position, distance, _myController._resizeBlockerMask))
                {
                    _myController._isPlayerSmall.Value = false;
                    _myController._sizeChangeEvent.Invoke();
                }
            }
            else
            {
                _myController._isPlayerSmall.Value = true;
                _myController._sizeChangeEvent.Invoke();
            }
        }

        public virtual PlayerCharacterState Attack()
        {
            return new PlayerAttackState(_myController);
        }

        public virtual PlayerCharacterState Dash()
        {
            return new PlayerDashState(_myController);
        }

        public virtual void Interact()
        {
            float interactionRange;
            if (_myController._isPlayerSmall.Value)
            {
                interactionRange = _myController._interactionRange.Value / _myController._sizeChangeFactor.Value;
            }
            else
            {
                interactionRange = _myController._interactionRange.Value;
            }
            Transform interactionTransform = _myController._interactionCheck;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(interactionTransform.position, interactionRange, _myController._interactionMask);
            foreach (Collider2D collider in colliders)
            {
                Interactable interactableObject = collider.gameObject.GetComponent<Interactable>();
                if (interactableObject != null)
                {
                    interactableObject.Interact(_myController._handTransform, ref _myController._pocket);
                }
            }
        }

        public virtual PlayerCharacterState StateFixedUpdate()
        {
            Rigidbody2D controlledBody = _myController._myBody;
            Animator controlledAnimator = _myController._myAnimator;
            LayerMask groundMask = _myController._whatIsGround;
            Transform groundCheck = _myController._groundCheck;
            if (!_myController._isGamePaused)
            {
                _myController._myMovement.Move(_myController._movementValue * _myController._currentMovementSpeed);
                controlledAnimator.SetFloat("walk_speed", Mathf.Abs(_myController._movementValue * _myController._currentMovementSpeed));
                controlledAnimator.SetFloat("jump_speed", controlledBody.velocity.y);
            }
            else
            {
                _myController._myMovement.Move(0.0f);
            }
            if (!CheckIfLanded(groundMask, groundCheck.position))
            {
                return new PlayerJumpState(_myController);
            }
            else
            {
                return null;
            }
        }

        public virtual void DrawGizmo()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(_myController.transform.position, 0.5f);
        }
    }
}