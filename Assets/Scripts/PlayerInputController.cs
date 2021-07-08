using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace GDS3
{
    public class PlayerInputController : MonoBehaviour, PlayerInput.IGameplayActions
    {
        [SerializeField] private CharacterBrain _myBrain;
        [SerializeField] private BoolReference _isInputBlocked;
        [SerializeField] private UnityEvent _pauseEvent;
        private PlayerInput _playerInput;
        private bool _isGamePaused;

        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Gameplay.SetCallbacks(this);
            _isInputBlocked.Value = false;
            _isGamePaused = false;
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
                _pauseEvent.Invoke();
                _isGamePaused = !_isGamePaused;
                _isInputBlocked.Value = _isGamePaused;
            }
        }
    }
}
