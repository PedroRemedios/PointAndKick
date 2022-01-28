// GENERATED AUTOMATICALLY FROM 'Assets/InputManager/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Exploration"",
            ""id"": ""9f0b27cc-1ae9-4481-b566-ce5dfaf796fa"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6d928a85-b8b7-4910-8335-d7770485ac13"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""02d29b17-5913-4117-81fa-7298bfe2deaf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""27f7edfb-a7a2-4779-8263-4b489d67c950"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""798cfdf2-f9f9-4ab3-921a-e00e86892198"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7ae2a99-56a0-4d6b-9d6a-99b43de3f92f"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - WASD Setup;Keyboard - Arrow Keys Setup"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e8cfbda3-5bda-4bcd-84a3-339bbcd0bd36"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard - WASD"",
                    ""id"": ""14a8c6c7-657a-4dc2-9177-2ed3a5eb940d"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""895f5623-20a8-43ad-86d3-66040df8f8c8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - WASD Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8e94f53b-5377-4577-a23f-8c3b176e0c5e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - WASD Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""74764b83-5fc7-443c-8a4e-1a803cdd83e3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - WASD Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cef39df5-76f0-4262-9065-bf6b9a3bd24e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - WASD Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard - Arrow Keys"",
                    ""id"": ""001f8bd6-2eec-4bb2-9132-c5bdcfa732b2"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d73afedc-ba78-4d11-aa4f-e43815e863d7"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - Arrow Keys Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7b7c1085-bb8b-4511-b7e0-ca17ceb1e9e6"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - Arrow Keys Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""56eb355a-6bc6-497f-9d6b-1661da7cbab4"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - Arrow Keys Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4708a732-8f68-4ea0-b573-6bc02e3c1eaf"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - Arrow Keys Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5effa0c3-3f93-495b-801d-154b0a9ba7e2"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ae6509d3-5901-46cb-9e87-c0942b557476"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - Arrow Keys Setup;Keyboard - WASD Setup"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Combat"",
            ""id"": ""eab3093d-d3da-4ac3-b67b-32f494d12e58"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""fff906d0-1384-4802-bc28-b7c240daf3b7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""1c43c8f2-ab28-47e8-abb5-d05d15ba5246"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""18e6ef0d-8aec-40ba-8430-09293a8e3190"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Light Attack"",
                    ""type"": ""Button"",
                    ""id"": ""3fe930bf-6e43-43a6-8790-3c7f5530e42c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Heavy Attack"",
                    ""type"": ""Button"",
                    ""id"": ""333eca50-88c6-42d8-b8eb-4a916e0ca009"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Diabolo Attack"",
                    ""type"": ""Button"",
                    ""id"": ""1b0ad733-89fc-4915-8bd7-10e3ad5f600b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Guard Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""fea17d8a-ae43-4f53-9b7d-d2a7d79f0a2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""005903cf-ffef-4431-b724-98477452d40a"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard - WASD"",
                    ""id"": ""1daee12d-d951-457c-af51-faee88fa776a"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""712995e9-20b9-421b-8421-3faf860da330"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - WASD Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d3465c49-5997-4329-aa4f-78486319b2b5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - WASD Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9b655e2d-76a0-48fa-ad84-4edcdca61b28"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - WASD Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""79138e68-b6a0-4db4-87c0-2760f9535da5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - WASD Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard - Arrow Keys"",
                    ""id"": ""ea897f75-f1fb-4326-8a20-a8f664d7acf3"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f0978237-13ca-48b7-bfe6-7e0621e5a145"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - Arrow Keys Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f58caee2-ecf0-4327-92c6-b53052f9639f"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - Arrow Keys Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f39806b5-8ae2-454e-9f07-08709f27c6b7"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - Arrow Keys Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d2421e1a-7a1d-4a6b-9f05-ac7d85267832"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - Arrow Keys Setup"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6e8deab2-3d66-49a3-9d37-f19319b3ddf5"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b91e0f2-eeae-4e4a-9d9a-5610006f3475"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - Arrow Keys Setup;Keyboard - WASD Setup"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94b1c578-f7e9-4223-b1ec-c1d5a88a5e86"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Light Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfad4b47-00b4-4a5a-afed-7b597437df3a"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - WASD Setup"",
                    ""action"": ""Light Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c22f26d5-7c2f-4c2f-a344-082a922678c2"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - Arrow Keys Setup"",
                    ""action"": ""Light Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11154a89-104f-47c0-9836-911babda2f2c"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Heavy Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2efb943-63f1-49e5-a0a6-ac8909a40bd4"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - WASD Setup"",
                    ""action"": ""Heavy Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc31959e-751c-47f2-956b-38d4ca986fac"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - Arrow Keys Setup"",
                    ""action"": ""Heavy Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4fcc9bc5-e181-4863-b332-ca6877c278de"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78d25150-bea5-40c5-aabc-8fa910ce9325"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - WASD Setup;Keyboard - Arrow Keys Setup"",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c98bf092-d892-4edd-a8a2-0178fea9f844"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Diabolo Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74632845-436b-4e29-8964-cf8ad1349b4b"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - WASD Setup"",
                    ""action"": ""Diabolo Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac5d8b48-6532-4ce2-99fd-6b57546fe28d"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard - Arrow Keys Setup"",
                    ""action"": ""Diabolo Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab779bf0-aea0-46e6-9cbd-3a5937c726e0"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Guard Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""ee2ccce4-f930-4980-99d3-ac55ca76d91a"",
            ""actions"": [],
            ""bindings"": []
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard - Arrow Keys Setup"",
            ""bindingGroup"": ""Keyboard - Arrow Keys Setup"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard - WASD Setup"",
            ""bindingGroup"": ""Keyboard - WASD Setup"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Exploration
        m_Exploration = asset.FindActionMap("Exploration", throwIfNotFound: true);
        m_Exploration_Move = m_Exploration.FindAction("Move", throwIfNotFound: true);
        m_Exploration_Run = m_Exploration.FindAction("Run", throwIfNotFound: true);
        m_Exploration_Jump = m_Exploration.FindAction("Jump", throwIfNotFound: true);
        // Combat
        m_Combat = asset.FindActionMap("Combat", throwIfNotFound: true);
        m_Combat_Move = m_Combat.FindAction("Move", throwIfNotFound: true);
        m_Combat_Run = m_Combat.FindAction("Run", throwIfNotFound: true);
        m_Combat_Jump = m_Combat.FindAction("Jump", throwIfNotFound: true);
        m_Combat_LightAttack = m_Combat.FindAction("Light Attack", throwIfNotFound: true);
        m_Combat_HeavyAttack = m_Combat.FindAction("Heavy Attack", throwIfNotFound: true);
        m_Combat_DiaboloAttack = m_Combat.FindAction("Diabolo Attack", throwIfNotFound: true);
        m_Combat_GuardDodge = m_Combat.FindAction("Guard Dodge", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
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

    // Exploration
    private readonly InputActionMap m_Exploration;
    private IExplorationActions m_ExplorationActionsCallbackInterface;
    private readonly InputAction m_Exploration_Move;
    private readonly InputAction m_Exploration_Run;
    private readonly InputAction m_Exploration_Jump;
    public struct ExplorationActions
    {
        private @PlayerInputActions m_Wrapper;
        public ExplorationActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Exploration_Move;
        public InputAction @Run => m_Wrapper.m_Exploration_Run;
        public InputAction @Jump => m_Wrapper.m_Exploration_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Exploration; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ExplorationActions set) { return set.Get(); }
        public void SetCallbacks(IExplorationActions instance)
        {
            if (m_Wrapper.m_ExplorationActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_ExplorationActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_ExplorationActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public ExplorationActions @Exploration => new ExplorationActions(this);

    // Combat
    private readonly InputActionMap m_Combat;
    private ICombatActions m_CombatActionsCallbackInterface;
    private readonly InputAction m_Combat_Move;
    private readonly InputAction m_Combat_Run;
    private readonly InputAction m_Combat_Jump;
    private readonly InputAction m_Combat_LightAttack;
    private readonly InputAction m_Combat_HeavyAttack;
    private readonly InputAction m_Combat_DiaboloAttack;
    private readonly InputAction m_Combat_GuardDodge;
    public struct CombatActions
    {
        private @PlayerInputActions m_Wrapper;
        public CombatActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Combat_Move;
        public InputAction @Run => m_Wrapper.m_Combat_Run;
        public InputAction @Jump => m_Wrapper.m_Combat_Jump;
        public InputAction @LightAttack => m_Wrapper.m_Combat_LightAttack;
        public InputAction @HeavyAttack => m_Wrapper.m_Combat_HeavyAttack;
        public InputAction @DiaboloAttack => m_Wrapper.m_Combat_DiaboloAttack;
        public InputAction @GuardDodge => m_Wrapper.m_Combat_GuardDodge;
        public InputActionMap Get() { return m_Wrapper.m_Combat; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CombatActions set) { return set.Get(); }
        public void SetCallbacks(ICombatActions instance)
        {
            if (m_Wrapper.m_CombatActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnJump;
                @LightAttack.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnLightAttack;
                @LightAttack.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnLightAttack;
                @LightAttack.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnLightAttack;
                @HeavyAttack.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnHeavyAttack;
                @DiaboloAttack.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnDiaboloAttack;
                @DiaboloAttack.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnDiaboloAttack;
                @DiaboloAttack.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnDiaboloAttack;
                @GuardDodge.started -= m_Wrapper.m_CombatActionsCallbackInterface.OnGuardDodge;
                @GuardDodge.performed -= m_Wrapper.m_CombatActionsCallbackInterface.OnGuardDodge;
                @GuardDodge.canceled -= m_Wrapper.m_CombatActionsCallbackInterface.OnGuardDodge;
            }
            m_Wrapper.m_CombatActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @LightAttack.started += instance.OnLightAttack;
                @LightAttack.performed += instance.OnLightAttack;
                @LightAttack.canceled += instance.OnLightAttack;
                @HeavyAttack.started += instance.OnHeavyAttack;
                @HeavyAttack.performed += instance.OnHeavyAttack;
                @HeavyAttack.canceled += instance.OnHeavyAttack;
                @DiaboloAttack.started += instance.OnDiaboloAttack;
                @DiaboloAttack.performed += instance.OnDiaboloAttack;
                @DiaboloAttack.canceled += instance.OnDiaboloAttack;
                @GuardDodge.started += instance.OnGuardDodge;
                @GuardDodge.performed += instance.OnGuardDodge;
                @GuardDodge.canceled += instance.OnGuardDodge;
            }
        }
    }
    public CombatActions @Combat => new CombatActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    public struct UIActions
    {
        private @PlayerInputActions m_Wrapper;
        public UIActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    private int m_KeyboardArrowKeysSetupSchemeIndex = -1;
    public InputControlScheme KeyboardArrowKeysSetupScheme
    {
        get
        {
            if (m_KeyboardArrowKeysSetupSchemeIndex == -1) m_KeyboardArrowKeysSetupSchemeIndex = asset.FindControlSchemeIndex("Keyboard - Arrow Keys Setup");
            return asset.controlSchemes[m_KeyboardArrowKeysSetupSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    private int m_KeyboardWASDSetupSchemeIndex = -1;
    public InputControlScheme KeyboardWASDSetupScheme
    {
        get
        {
            if (m_KeyboardWASDSetupSchemeIndex == -1) m_KeyboardWASDSetupSchemeIndex = asset.FindControlSchemeIndex("Keyboard - WASD Setup");
            return asset.controlSchemes[m_KeyboardWASDSetupSchemeIndex];
        }
    }
    public interface IExplorationActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface ICombatActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLightAttack(InputAction.CallbackContext context);
        void OnHeavyAttack(InputAction.CallbackContext context);
        void OnDiaboloAttack(InputAction.CallbackContext context);
        void OnGuardDodge(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
    }
}
