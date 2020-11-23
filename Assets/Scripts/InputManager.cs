
using UnityEngine;
using UnityEngine.InputSystem;

// Use a separate InputControl component for setting up input.
public class InputManager : MonoSingleton<InputManager>, SimpleControls.IGameplayActions {
    public SimpleControls controls;
    private void Awake()
    {
        controls = new SimpleControls();
        controls.gameplay.SetCallbacks(this);
    }

    // Start is called before the first frame update

    public void OnEnable()
    {
        Debug.Log("Enabling gameplay controls!");
        controls.gameplay.Enable();
    }

    public void OnDisable()
    {
        Debug.Log("Disabling gameplay controls!");
        controls.gameplay.Disable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 original = context.ReadValue<Vector2>();
        Aviator.mousePos = new Vector2(2f * original.x / Screen.width - 1, 2f * original.y / Screen.height - 1);
        print("OnMove" + Aviator.mousePos);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        // TODO check if onfire be called twice
        if (Aviator.status == AviatorStates.Start) {
            Aviator.reset();
            print("OnFire" + Aviator.mousePos);
            Aviator.status = AviatorStates.Flying;
        }
    }

    // public void OnGUI()
    // {
    //     if (m_Charging)
    //         GUI.Label(new Rect(100, 100, 200, 100), "Charging...");
    // }

}
