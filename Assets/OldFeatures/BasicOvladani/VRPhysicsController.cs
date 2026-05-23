using UnityEngine;
using UnityEngine.InputSystem;

public class VRPhysicsController : MonoBehaviour {

    [Header("XR components")]
    public Transform HMD;
    public Transform ControllerL;
    public Transform ControllerR;

    [Header("Physics proxy components")]
    public CapsuleCollider Collider;
    public Rigidbody BodyRB;
    public Rigidbody HandLeftProxy;
    public Rigidbody HandRightProxy;
    public Rigidbody HeadProxy;

    [Header("Values")]
    public float MinColliderHeight = 0.5f;
    public float MaxColliderHeight = 2.5f;
    public float MovementSpeed = 3f;
    public InputActionReference LocomotionInput;

    private void FixedUpdate() {
        CorrectPlayerHeight();
        PositionProxyComponents();
        MoveCharacterRigidbody();
    }

    void CorrectPlayerHeight() {
        Collider.height = Mathf.Clamp(HMD.localPosition.y, MinColliderHeight, MaxColliderHeight);
        Collider.center = new Vector3(
            HMD.localPosition.x,
            HMD.localPosition.y/2,
            HMD.localPosition.z
        );
    }

    void PositionProxyComponents() {
        HandLeftProxy.MovePosition(ControllerL.transform.position);
        HandLeftProxy.MoveRotation(ControllerL.transform.rotation);

        HandRightProxy.MovePosition(ControllerR.transform.position);
        HandRightProxy.MoveRotation(ControllerR.transform.rotation);

        HeadProxy.MovePosition(HMD.transform.position);
        HeadProxy.MoveRotation(HMD.transform.rotation);
    }

    void MoveCharacterRigidbody() {
        Vector2 joy = LocomotionInput.action.ReadValue<Vector2>();
        if (joy.sqrMagnitude < 0.01f) return;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * joy.y + right * joy.x;
        moveDir.Normalize();

        Vector3 targetVelocity = moveDir * MovementSpeed;
        Vector3 currentVelocity = BodyRB.linearVelocity;

        BodyRB.linearVelocity = new Vector3(
            targetVelocity.x,
            currentVelocity.y,
            targetVelocity.z
        );
    }
}
