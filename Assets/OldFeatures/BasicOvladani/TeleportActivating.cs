using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TeleportActivating : MonoBehaviour {

    public XRRayInteractor teleportInteractor;
    [SerializeField]
    public InputActionReference teleportActivatorAction;

    void Start() {
        teleportInteractor.gameObject.SetActive(false);
        teleportActivatorAction.action.performed += Action_performed;
    }

    void Update() {
        if (teleportActivatorAction.action.WasReleasedThisFrame()) {
            teleportInteractor.gameObject.SetActive(false);
        }
    }

    private void Action_performed(InputAction.CallbackContext obj) {
        teleportInteractor.gameObject.SetActive(true);
    }
}
