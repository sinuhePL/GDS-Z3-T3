using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GDS3
{
    public class PlayerInputController : MonoBehaviour, PlayerInput.IGameplayActions
    {
        [SerializeField] private CharacterBrain _myBrain;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Gameplay.SetCallbacks(this);
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            _myBrain._movementValue = context.ReadValue<float>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                _myBrain._jumpPressed = true;
            }
        }

        public void OnResize(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _myBrain._resizePressed = true;
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _myBrain._attackPressed = true;
            }
        }

        public void OnDashLeft(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _myBrain._dashValue = -1.0f;
            }
        }

        public void OnDashRight(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _myBrain._dashValue = 1.0f;
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _myBrain._interactPressed = true;
            }
        }
    }
}
