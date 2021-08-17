// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Model/Settings/MainInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MainInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainInput"",
    ""maps"": [
        {
            ""name"": ""MainAction"",
            ""id"": ""31ddb672-8458-44ad-9559-66fc63a406e6"",
            ""actions"": [
                {
                    ""name"": ""Normal"",
                    ""type"": ""Button"",
                    ""id"": ""26b9042d-c093-4195-bb67-90ff07f9c6da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GPU"",
                    ""type"": ""Button"",
                    ""id"": ""a5aa77c1-d77d-4044-aa9c-1c2efdeb8793"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9d973852-999b-4863-9645-835bb4fc48b6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Normal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95af1f46-d0e5-48d3-868f-70147d83acb5"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GPU"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""MainScheme"",
            ""bindingGroup"": ""MainScheme"",
            ""devices"": []
        }
    ]
}");
        // MainAction
        m_MainAction = asset.FindActionMap("MainAction", throwIfNotFound: true);
        m_MainAction_Normal = m_MainAction.FindAction("Normal", throwIfNotFound: true);
        m_MainAction_GPU = m_MainAction.FindAction("GPU", throwIfNotFound: true);
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

    // MainAction
    private readonly InputActionMap m_MainAction;
    private IMainActionActions m_MainActionActionsCallbackInterface;
    private readonly InputAction m_MainAction_Normal;
    private readonly InputAction m_MainAction_GPU;
    public struct MainActionActions
    {
        private @MainInput m_Wrapper;
        public MainActionActions(@MainInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Normal => m_Wrapper.m_MainAction_Normal;
        public InputAction @GPU => m_Wrapper.m_MainAction_GPU;
        public InputActionMap Get() { return m_Wrapper.m_MainAction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActionActions set) { return set.Get(); }
        public void SetCallbacks(IMainActionActions instance)
        {
            if (m_Wrapper.m_MainActionActionsCallbackInterface != null)
            {
                @Normal.started -= m_Wrapper.m_MainActionActionsCallbackInterface.OnNormal;
                @Normal.performed -= m_Wrapper.m_MainActionActionsCallbackInterface.OnNormal;
                @Normal.canceled -= m_Wrapper.m_MainActionActionsCallbackInterface.OnNormal;
                @GPU.started -= m_Wrapper.m_MainActionActionsCallbackInterface.OnGPU;
                @GPU.performed -= m_Wrapper.m_MainActionActionsCallbackInterface.OnGPU;
                @GPU.canceled -= m_Wrapper.m_MainActionActionsCallbackInterface.OnGPU;
            }
            m_Wrapper.m_MainActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Normal.started += instance.OnNormal;
                @Normal.performed += instance.OnNormal;
                @Normal.canceled += instance.OnNormal;
                @GPU.started += instance.OnGPU;
                @GPU.performed += instance.OnGPU;
                @GPU.canceled += instance.OnGPU;
            }
        }
    }
    public MainActionActions @MainAction => new MainActionActions(this);
    private int m_MainSchemeSchemeIndex = -1;
    public InputControlScheme MainSchemeScheme
    {
        get
        {
            if (m_MainSchemeSchemeIndex == -1) m_MainSchemeSchemeIndex = asset.FindControlSchemeIndex("MainScheme");
            return asset.controlSchemes[m_MainSchemeSchemeIndex];
        }
    }
    public interface IMainActionActions
    {
        void OnNormal(InputAction.CallbackContext context);
        void OnGPU(InputAction.CallbackContext context);
    }
}
