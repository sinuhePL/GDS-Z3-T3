using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GDS3
{
    public class CursorLightController : MonoBehaviour
    {
        private Camera _myCamera;
        private GDS3.PlayerInput _myPlayerInput;
        private InputAction _mouseMovement;

        private void Awake()
        {
            _myPlayerInput = new PlayerInput();
            _mouseMovement = _myPlayerInput.Gameplay.MousePosition;
            _mouseMovement.performed += OnMouseMovement;
            _mouseMovement.canceled += OnMouseMovement;
        }

        public void OnMouseMovement(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Vector2 mousePosition = context.ReadValue<Vector2>();
                Vector3 projectedMousePosition = _myCamera.ScreenToWorldPoint(mousePosition);
                transform.position = new Vector3(projectedMousePosition.x, projectedMousePosition.y, 0.5f);
            }
        }

        private void OnEnable()
        {
            _mouseMovement.Enable();
        }

        private void OnDisable()
        {
            _mouseMovement.Disable();
        }

        void Start()
        {
            _myCamera = Camera.main;
        }
    }
}
