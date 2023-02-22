//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Upgrade/Controllers/TouchControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace LittleMars.Controllers
{
    public partial class @TouchControls : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @TouchControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""TouchControls"",
    ""maps"": [
        {
            ""name"": ""ViewActionMap"",
            ""id"": ""30f3792e-d5ce-42ec-991f-36d2ed1224f4"",
            ""actions"": [
                {
                    ""name"": ""PrimaryTouchPosition"",
                    ""type"": ""Value"",
                    ""id"": ""92670e06-40d2-4ab9-a724-6831e8b4f7dd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PrimaryTouchContact"",
                    ""type"": ""Button"",
                    ""id"": ""ec541d24-0bbd-491f-8f24-329d62609219"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondaryTouchPosition"",
                    ""type"": ""Value"",
                    ""id"": ""b101bad4-5668-4c5d-80b8-72f99676edc8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SecondaryTouchContact"",
                    ""type"": ""Button"",
                    ""id"": ""31680511-19b0-4db5-9b41-f1d082db481b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScrollWheelYDirection"",
                    ""type"": ""Value"",
                    ""id"": ""71c809ca-84b2-4d64-a482-31c3b25a4a15"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0770b6c7-6277-4b28-bdb8-9ede70024a15"",
                    ""path"": ""<Touchscreen>/primaryTouch/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryTouchContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d156b29a-5092-4ae1-a940-d036e7ba69f8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryTouchContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b57c3a17-b6ce-4903-83e1-559c812c601e"",
                    ""path"": ""<Touchscreen>/touch0/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryTouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6d2c5a7d-7788-49a2-89fc-9ba25192dd75"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryTouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8aa21710-0ea9-4f90-9e27-448859d85dbf"",
                    ""path"": ""<Touchscreen>/primaryTouch/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryTouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b9f054e-357e-4b04-bf2a-61f19f0173aa"",
                    ""path"": ""<Touchscreen>/touch1/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryTouchPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa9f6d75-ccbc-43a1-bf1d-7f0043129f12"",
                    ""path"": ""<Touchscreen>/touch1/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryTouchContact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9538c98e-405e-45ed-bf8a-5bf916c3a0fa"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ScrollWheelYDirection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // ViewActionMap
            m_ViewActionMap = asset.FindActionMap("ViewActionMap", throwIfNotFound: true);
            m_ViewActionMap_PrimaryTouchPosition = m_ViewActionMap.FindAction("PrimaryTouchPosition", throwIfNotFound: true);
            m_ViewActionMap_PrimaryTouchContact = m_ViewActionMap.FindAction("PrimaryTouchContact", throwIfNotFound: true);
            m_ViewActionMap_SecondaryTouchPosition = m_ViewActionMap.FindAction("SecondaryTouchPosition", throwIfNotFound: true);
            m_ViewActionMap_SecondaryTouchContact = m_ViewActionMap.FindAction("SecondaryTouchContact", throwIfNotFound: true);
            m_ViewActionMap_ScrollWheelYDirection = m_ViewActionMap.FindAction("ScrollWheelYDirection", throwIfNotFound: true);
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
        public IEnumerable<InputBinding> bindings => asset.bindings;

        public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
        {
            return asset.FindAction(actionNameOrId, throwIfNotFound);
        }
        public int FindBinding(InputBinding bindingMask, out InputAction action)
        {
            return asset.FindBinding(bindingMask, out action);
        }

        // ViewActionMap
        private readonly InputActionMap m_ViewActionMap;
        private IViewActionMapActions m_ViewActionMapActionsCallbackInterface;
        private readonly InputAction m_ViewActionMap_PrimaryTouchPosition;
        private readonly InputAction m_ViewActionMap_PrimaryTouchContact;
        private readonly InputAction m_ViewActionMap_SecondaryTouchPosition;
        private readonly InputAction m_ViewActionMap_SecondaryTouchContact;
        private readonly InputAction m_ViewActionMap_ScrollWheelYDirection;
        public struct ViewActionMapActions
        {
            private @TouchControls m_Wrapper;
            public ViewActionMapActions(@TouchControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @PrimaryTouchPosition => m_Wrapper.m_ViewActionMap_PrimaryTouchPosition;
            public InputAction @PrimaryTouchContact => m_Wrapper.m_ViewActionMap_PrimaryTouchContact;
            public InputAction @SecondaryTouchPosition => m_Wrapper.m_ViewActionMap_SecondaryTouchPosition;
            public InputAction @SecondaryTouchContact => m_Wrapper.m_ViewActionMap_SecondaryTouchContact;
            public InputAction @ScrollWheelYDirection => m_Wrapper.m_ViewActionMap_ScrollWheelYDirection;
            public InputActionMap Get() { return m_Wrapper.m_ViewActionMap; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ViewActionMapActions set) { return set.Get(); }
            public void SetCallbacks(IViewActionMapActions instance)
            {
                if (m_Wrapper.m_ViewActionMapActionsCallbackInterface != null)
                {
                    @PrimaryTouchPosition.started -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnPrimaryTouchPosition;
                    @PrimaryTouchPosition.performed -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnPrimaryTouchPosition;
                    @PrimaryTouchPosition.canceled -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnPrimaryTouchPosition;
                    @PrimaryTouchContact.started -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnPrimaryTouchContact;
                    @PrimaryTouchContact.performed -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnPrimaryTouchContact;
                    @PrimaryTouchContact.canceled -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnPrimaryTouchContact;
                    @SecondaryTouchPosition.started -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnSecondaryTouchPosition;
                    @SecondaryTouchPosition.performed -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnSecondaryTouchPosition;
                    @SecondaryTouchPosition.canceled -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnSecondaryTouchPosition;
                    @SecondaryTouchContact.started -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnSecondaryTouchContact;
                    @SecondaryTouchContact.performed -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnSecondaryTouchContact;
                    @SecondaryTouchContact.canceled -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnSecondaryTouchContact;
                    @ScrollWheelYDirection.started -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnScrollWheelYDirection;
                    @ScrollWheelYDirection.performed -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnScrollWheelYDirection;
                    @ScrollWheelYDirection.canceled -= m_Wrapper.m_ViewActionMapActionsCallbackInterface.OnScrollWheelYDirection;
                }
                m_Wrapper.m_ViewActionMapActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @PrimaryTouchPosition.started += instance.OnPrimaryTouchPosition;
                    @PrimaryTouchPosition.performed += instance.OnPrimaryTouchPosition;
                    @PrimaryTouchPosition.canceled += instance.OnPrimaryTouchPosition;
                    @PrimaryTouchContact.started += instance.OnPrimaryTouchContact;
                    @PrimaryTouchContact.performed += instance.OnPrimaryTouchContact;
                    @PrimaryTouchContact.canceled += instance.OnPrimaryTouchContact;
                    @SecondaryTouchPosition.started += instance.OnSecondaryTouchPosition;
                    @SecondaryTouchPosition.performed += instance.OnSecondaryTouchPosition;
                    @SecondaryTouchPosition.canceled += instance.OnSecondaryTouchPosition;
                    @SecondaryTouchContact.started += instance.OnSecondaryTouchContact;
                    @SecondaryTouchContact.performed += instance.OnSecondaryTouchContact;
                    @SecondaryTouchContact.canceled += instance.OnSecondaryTouchContact;
                    @ScrollWheelYDirection.started += instance.OnScrollWheelYDirection;
                    @ScrollWheelYDirection.performed += instance.OnScrollWheelYDirection;
                    @ScrollWheelYDirection.canceled += instance.OnScrollWheelYDirection;
                }
            }
        }
        public ViewActionMapActions @ViewActionMap => new ViewActionMapActions(this);
        public interface IViewActionMapActions
        {
            void OnPrimaryTouchPosition(InputAction.CallbackContext context);
            void OnPrimaryTouchContact(InputAction.CallbackContext context);
            void OnSecondaryTouchPosition(InputAction.CallbackContext context);
            void OnSecondaryTouchContact(InputAction.CallbackContext context);
            void OnScrollWheelYDirection(InputAction.CallbackContext context);
        }
    }
}