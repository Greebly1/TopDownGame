//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Player Input/CameraControls.inputactions
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

public partial class @CameraControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @CameraControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CameraControls"",
    ""maps"": [
        {
            ""name"": ""Adventure"",
            ""id"": ""f8edbe88-fbea-4c40-b29e-38fb18e5adfa"",
            ""actions"": [
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""030f167d-0d3d-4b6c-af04-79f63c436adb"",
                    ""expectedControlType"": ""Delta"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""AlignYRotation"",
                    ""type"": ""Button"",
                    ""id"": ""021b71bf-a4b9-4f1b-a5df-29eac0896820"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""QuickTurn"",
                    ""type"": ""Button"",
                    ""id"": ""03285ba6-b3d0-4ce0-a67d-c55eec6ab41b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CamRotation"",
                    ""type"": ""Value"",
                    ""id"": ""9f1ec4fc-47d7-473e-91f3-7ec8ac726286"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8c8c1032-c723-4e72-b668-49978488eadb"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07570c39-0888-4a26-895a-cf29720256bf"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": ""Hold(duration=0.2)"",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""AlignYRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""808952cb-ed87-4e4c-90ff-276d91815ccb"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""QuickTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""ec2e7144-b353-49f7-8c8d-1fdcb71104f7"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CamRotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""458d2e14-ce3e-46b3-9a1b-51286d8055e0"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""CamRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""20f609a8-677d-46b7-8b1b-938fa8511c84"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""M&K"",
                    ""action"": ""CamRotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""M&K"",
            ""bindingGroup"": ""M&K"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Adventure
        m_Adventure = asset.FindActionMap("Adventure", throwIfNotFound: true);
        m_Adventure_Zoom = m_Adventure.FindAction("Zoom", throwIfNotFound: true);
        m_Adventure_AlignYRotation = m_Adventure.FindAction("AlignYRotation", throwIfNotFound: true);
        m_Adventure_QuickTurn = m_Adventure.FindAction("QuickTurn", throwIfNotFound: true);
        m_Adventure_CamRotation = m_Adventure.FindAction("CamRotation", throwIfNotFound: true);
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

    // Adventure
    private readonly InputActionMap m_Adventure;
    private List<IAdventureActions> m_AdventureActionsCallbackInterfaces = new List<IAdventureActions>();
    private readonly InputAction m_Adventure_Zoom;
    private readonly InputAction m_Adventure_AlignYRotation;
    private readonly InputAction m_Adventure_QuickTurn;
    private readonly InputAction m_Adventure_CamRotation;
    public struct AdventureActions
    {
        private @CameraControls m_Wrapper;
        public AdventureActions(@CameraControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Zoom => m_Wrapper.m_Adventure_Zoom;
        public InputAction @AlignYRotation => m_Wrapper.m_Adventure_AlignYRotation;
        public InputAction @QuickTurn => m_Wrapper.m_Adventure_QuickTurn;
        public InputAction @CamRotation => m_Wrapper.m_Adventure_CamRotation;
        public InputActionMap Get() { return m_Wrapper.m_Adventure; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AdventureActions set) { return set.Get(); }
        public void AddCallbacks(IAdventureActions instance)
        {
            if (instance == null || m_Wrapper.m_AdventureActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_AdventureActionsCallbackInterfaces.Add(instance);
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
            @AlignYRotation.started += instance.OnAlignYRotation;
            @AlignYRotation.performed += instance.OnAlignYRotation;
            @AlignYRotation.canceled += instance.OnAlignYRotation;
            @QuickTurn.started += instance.OnQuickTurn;
            @QuickTurn.performed += instance.OnQuickTurn;
            @QuickTurn.canceled += instance.OnQuickTurn;
            @CamRotation.started += instance.OnCamRotation;
            @CamRotation.performed += instance.OnCamRotation;
            @CamRotation.canceled += instance.OnCamRotation;
        }

        private void UnregisterCallbacks(IAdventureActions instance)
        {
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
            @AlignYRotation.started -= instance.OnAlignYRotation;
            @AlignYRotation.performed -= instance.OnAlignYRotation;
            @AlignYRotation.canceled -= instance.OnAlignYRotation;
            @QuickTurn.started -= instance.OnQuickTurn;
            @QuickTurn.performed -= instance.OnQuickTurn;
            @QuickTurn.canceled -= instance.OnQuickTurn;
            @CamRotation.started -= instance.OnCamRotation;
            @CamRotation.performed -= instance.OnCamRotation;
            @CamRotation.canceled -= instance.OnCamRotation;
        }

        public void RemoveCallbacks(IAdventureActions instance)
        {
            if (m_Wrapper.m_AdventureActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IAdventureActions instance)
        {
            foreach (var item in m_Wrapper.m_AdventureActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_AdventureActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public AdventureActions @Adventure => new AdventureActions(this);
    private int m_MKSchemeIndex = -1;
    public InputControlScheme MKScheme
    {
        get
        {
            if (m_MKSchemeIndex == -1) m_MKSchemeIndex = asset.FindControlSchemeIndex("M&K");
            return asset.controlSchemes[m_MKSchemeIndex];
        }
    }
    public interface IAdventureActions
    {
        void OnZoom(InputAction.CallbackContext context);
        void OnAlignYRotation(InputAction.CallbackContext context);
        void OnQuickTurn(InputAction.CallbackContext context);
        void OnCamRotation(InputAction.CallbackContext context);
    }
}
