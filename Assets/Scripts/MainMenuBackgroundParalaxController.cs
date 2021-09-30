using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GDS3
{
    public class MainMenuBackgroundParalaxController : MonoBehaviour
    {
        [SerializeField] private float _paralaxFactor;
        private GDS3.PlayerInput _myPlayerInput;
        private InputAction _mouseMovement;
        private Vector3 _initialPosition;

        private void Awake()
        {
            _myPlayerInput = new PlayerInput();
            _mouseMovement = _myPlayerInput.Gameplay.MousePosition;
            _mouseMovement.performed += OnMouseMovement;
            _mouseMovement.canceled += OnMouseMovement;
            _initialPosition = transform.position;
        }

        public void OnMouseMovement(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Vector2 mousePosition = context.ReadValue<Vector2>();
                Vector2 mouseDistanceFromCenter = new Vector2(Screen.width/2 - mousePosition.x, Screen.height / 2 - mousePosition.y);
                transform.position = new Vector3(_initialPosition.x + mouseDistanceFromCenter.x*_paralaxFactor , _initialPosition.y + mouseDistanceFromCenter.y*_paralaxFactor, _initialPosition.z);
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
    }
}
