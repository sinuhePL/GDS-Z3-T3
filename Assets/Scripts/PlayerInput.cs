// GENERATED AUTOMATICALLY FROM 'Assets/Player Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace GDS3
{
    public class @PlayerInput : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Controls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""71bf63bc-e3b3-4546-88b4-c449c3ab26c6"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""21d2845b-5203-483a-a52b-3caf7b210f83"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""53de872a-aa50-41ec-889f-81fbb955ac7f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Resize"",
                    ""type"": ""Button"",
                    ""id"": ""1f22a6c1-4fe5-4378-b634-8709b1ac867f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""8a91fbe5-1bfd-4bf9-a4e5-f2265103a34a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DashLeft"",
                    ""type"": ""Button"",
                    ""id"": ""b5fdb586-33ce-4b65-9632-c64e5ccb4ef6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DashRight"",
                    ""type"": ""Button"",
                    ""id"": ""8d09db8a-0abd-4bfd-82f3-8a392e656e83"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""2602a6e6-39c6-4a76-a9b7-2457e1f704f8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Horizontal"",
                    ""id"": ""ee8e9210-999c-4511-b13e-a06dbdf4fd7f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3bdccf1a-785c-45e7-9da5-35e535cbff8e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""44576d76-9730-438f-9cfd-8b47d9976e9f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4c960d92-1066-4024-9b78-baa196b96663"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f40b8a07-ad26-4273-835f-412d7309cc0c"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Resize"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""281bb73d-e963-40be-91d6-e58bab2b6243"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f22254a-b156-4431-844e-19e68620fd06"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a032532-4b9f-4f8c-a7d5-c897c8659618"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DashRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""307a14a6-6e91-43c9-bc0d-5dab2af8124a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
            m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
            m_Gameplay_Resize = m_Gameplay.FindAction("Resize", throwIfNotFound: true);
            m_Gameplay_Attack = m_Gameplay.FindAction("Attack", throwIfNotFound: true);
            m_Gameplay_DashLeft = m_Gameplay.FindAction("DashLeft", throwIfNotFound: true);
            m_Gameplay_DashRight = m_Gameplay.FindAction("DashRight", throwIfNotFound: true);
            m_Gameplay_Interact = m_Gameplay.FindAction("Interact", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_Movement;
        private readonly InputAction m_Gameplay_Jump;
        private readonly InputAction m_Gameplay_Resize;
        private readonly InputAction m_Gameplay_Attack;
        private readonly InputAction m_Gameplay_DashLeft;
        private readonly InputAction m_Gameplay_DashRight;
        private readonly InputAction m_Gameplay_Interact;
        public struct GameplayActions
        {
            private @PlayerInput m_Wrapper;
            public GameplayActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
            public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
            public InputAction @Resize => m_Wrapper.m_Gameplay_Resize;
            public InputAction @Attack => m_Wrapper.m_Gameplay_Attack;
            public InputAction @DashLeft => m_Wrapper.m_Gameplay_DashLeft;
            public InputAction @DashRight => m_Wrapper.m_Gameplay_DashRight;
            public InputAction @Interact => m_Wrapper.m_Gameplay_Interact;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Resize.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnResize;
                    @Resize.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnResize;
                    @Resize.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnResize;
                    @Attack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                    @Attack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                    @Attack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                    @DashLeft.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDashLeft;
                    @DashLeft.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDashLeft;
                    @DashLeft.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDashLeft;
                    @DashRight.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDashRight;
                    @DashRight.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDashRight;
                    @DashRight.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDashRight;
                    @Interact.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                    @Interact.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                    @Interact.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Resize.started += instance.OnResize;
                    @Resize.performed += instance.OnResize;
                    @Resize.canceled += instance.OnResize;
                    @Attack.started += instance.OnAttack;
                    @Attack.performed += instance.OnAttack;
                    @Attack.canceled += instance.OnAttack;
                    @DashLeft.started += instance.OnDashLeft;
                    @DashLeft.performed += instance.OnDashLeft;
                    @DashLeft.canceled += instance.OnDashLeft;
                    @DashRight.started += instance.OnDashRight;
                    @DashRight.performed += instance.OnDashRight;
                    @DashRight.canceled += instance.OnDashRight;
                    @Interact.started += instance.OnInteract;
                    @Interact.performed += instance.OnInteract;
                    @Interact.canceled += instance.OnInteract;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);
        public interface IGameplayActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnResize(InputAction.CallbackContext context);
            void OnAttack(InputAction.CallbackContext context);
            void OnDashLeft(InputAction.CallbackContext context);
            void OnDashRight(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
        }
    }
}
