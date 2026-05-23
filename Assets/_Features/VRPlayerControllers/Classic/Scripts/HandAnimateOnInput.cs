using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimateOnInput : MonoBehaviour {

    public InputActionProperty TriggerInput;
    public InputActionProperty GripInput;

    Animator _animator;

    private void Awake() {
        _animator = GetComponent<Animator>();

        TriggerInput.action.Enable();
        GripInput.action.Enable();
    }

    private void Update() {
        float trigger = TriggerInput.action.ReadValue<float>();
        float grip = GripInput.action.ReadValue<float>();

        _animator.SetFloat("Trigger", trigger);
        _animator.SetFloat("Grip", grip);

    }

}
