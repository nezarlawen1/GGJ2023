//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Input/PlayerInput.inputactions
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

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""1a89f354-4a36-44bd-b017-28a7473112a9"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""0015eafc-7481-468a-b9b8-655f85d21df7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""6364fb77-6d18-4a37-a714-14db8a6fe77e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SwapToScout"",
                    ""type"": ""Button"",
                    ""id"": ""74104f9d-8b17-4c28-a4c7-7f7ed9f5518a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.3,pressPoint=0.4)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootPress"",
                    ""type"": ""Button"",
                    ""id"": ""0e7adb01-de6c-4336-8ab6-c0df4da5fc45"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.2,behavior=2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShootHold"",
                    ""type"": ""Button"",
                    ""id"": ""3d75d50c-38e9-4bbd-8c9f-aa0ab4d64d26"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.3,pressPoint=0.4)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Enteract"",
                    ""type"": ""Button"",
                    ""id"": ""b8e35c1d-3ac4-42af-a62d-b4f6300e10f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.2,behavior=2)"",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""2fe875d9-737c-4e04-a64e-2b94e592556f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""92660051-1c8f-4f85-961f-fa012d5d204c"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c357d116-a31d-4165-93d7-be5f80d6be4b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9f8d3f3e-b6e8-4d19-9ea6-131de8a72e22"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b25a2c7e-426e-4935-b26d-edd4a8d429e4"",
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
                    ""id"": ""ead682c7-6fb1-42cd-8b63-e1d7cf7a9d1b"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb1c7117-db8a-40b5-9077-f3f44df12104"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwapToScout"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e2558e8-5cc0-4006-9ff9-4075b8a2c1a3"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a3cff05f-71b1-47dc-93d3-e984338e002e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ShootHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""718318d9-382f-41f6-9ded-b1ce639a0db3"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_SwapToScout = m_Player.FindAction("SwapToScout", throwIfNotFound: true);
        m_Player_ShootPress = m_Player.FindAction("ShootPress", throwIfNotFound: true);
        m_Player_ShootHold = m_Player.FindAction("ShootHold", throwIfNotFound: true);
        m_Player_Enteract = m_Player.FindAction("Enteract", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_SwapToScout;
    private readonly InputAction m_Player_ShootPress;
    private readonly InputAction m_Player_ShootHold;
    private readonly InputAction m_Player_Enteract;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @SwapToScout => m_Wrapper.m_Player_SwapToScout;
        public InputAction @ShootPress => m_Wrapper.m_Player_ShootPress;
        public InputAction @ShootHold => m_Wrapper.m_Player_ShootHold;
        public InputAction @Enteract => m_Wrapper.m_Player_Enteract;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @SwapToScout.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapToScout;
                @SwapToScout.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapToScout;
                @SwapToScout.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwapToScout;
                @ShootPress.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootPress;
                @ShootPress.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootPress;
                @ShootPress.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootPress;
                @ShootHold.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootHold;
                @ShootHold.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootHold;
                @ShootHold.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShootHold;
                @Enteract.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnteract;
                @Enteract.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnteract;
                @Enteract.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnEnteract;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @SwapToScout.started += instance.OnSwapToScout;
                @SwapToScout.performed += instance.OnSwapToScout;
                @SwapToScout.canceled += instance.OnSwapToScout;
                @ShootPress.started += instance.OnShootPress;
                @ShootPress.performed += instance.OnShootPress;
                @ShootPress.canceled += instance.OnShootPress;
                @ShootHold.started += instance.OnShootHold;
                @ShootHold.performed += instance.OnShootHold;
                @ShootHold.canceled += instance.OnShootHold;
                @Enteract.started += instance.OnEnteract;
                @Enteract.performed += instance.OnEnteract;
                @Enteract.canceled += instance.OnEnteract;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnSwapToScout(InputAction.CallbackContext context);
        void OnShootPress(InputAction.CallbackContext context);
        void OnShootHold(InputAction.CallbackContext context);
        void OnEnteract(InputAction.CallbackContext context);
    }
}
