using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;
using UnityEngine.XR.OpenXR.Input;

public class HapticInvoker : MonoBehaviour {

    [Range(0f, 1f)]
    public float Amplitude = 0.7f;
    [Range(0f, 2f)]
    public float Duration = 0.2f;
    [Range(0f, 1f)]
    public float Frequency = 0.2f;

    public HapticImpulsePlayer HapticLeft;
    public HapticImpulsePlayer HapticRight;
    public InputActionReference TriggerLeft;
    public InputActionReference TriggerRight;

    public void TriggerHapticLeft() {
        HapticLeft.SendHapticImpulse(Amplitude, Duration, Frequency);
    }

    public void TriggerHapticRight() {
        HapticRight.SendHapticImpulse(Amplitude, Duration, Frequency);
    }

    void OnLeftTrigger(InputAction.CallbackContext ctx) {
        TriggerHapticLeft();
    }

    void OnRightTrigger(InputAction.CallbackContext ctx) {
        TriggerHapticRight();
    }

    void OnEnable() {
        TriggerLeft.action.Enable();
        TriggerRight.action.Enable();

        TriggerLeft.action.performed += OnLeftTrigger;
        TriggerRight.action.performed += OnRightTrigger;
    }

}
