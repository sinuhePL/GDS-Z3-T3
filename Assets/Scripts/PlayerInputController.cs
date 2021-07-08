using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GDS3
{
    public class PlayerInputController : MonoBehaviour, PlayerInput.IGameplayActions
    {
        [SerializeField] private CharacterBrain _myBrain;
        [SerializeField] private BoolReference _isInputBlocked;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Gameplay.SetCallbacks(this);
            _isInputBlocked.Value = false;
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
            if (!_isInputBlocked.Value)
            {
                _myBrain._movementValue = context.ReadValue<float>();
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed && !_isInputBlocked.Value)
            {
                _myBrain._jumpPressed = true;
            }
        }

        public void OnResize(InputAction.CallbackContext context)
        {
            if (context.performed && !_isInputBlocked.Value)
            {
                _myBrain._resizePressed = true;
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed && !_isInputBlocked.Value)
            {
                _myBrain._attackPressed = true;
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed && !_isInputBlocked.Value)
            {
                _myBrain._dashPressed = true;
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed && !_isInputBlocked.Value)
            {
                _myBrain._interactPressed = true;
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {

            }
        }
    }
}
