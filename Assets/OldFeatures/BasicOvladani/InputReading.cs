using UnityEngine;
using UnityEngine.InputSystem;

public class InputReading : MonoBehaviour {

    public Transform LeftHandRefference;
    public Transform RightHandRefference;
    public GameObject BulletPrefab;
    public float AttackSpeed;
    public float SpawnOffset;

    public InputActionProperty InputTriggerL;
    public InputActionProperty InputTriggerR;
    public InputActionProperty InputGrabL;
    public InputActionProperty InputGrabR;

    public InputActionProperty RightJoystick;
    public CharacterController controller;
    public Rigidbody rb;
    public float moveSpeed = 2f;
    private float leftCooldown = 0f;
    private float rightCooldown = 0f;

    private void Start() {
        InputTriggerL.action.Enable();
        InputTriggerR.action.Enable();
        InputGrabL.action.Enable();
        InputGrabR.action.Enable();
        RightJoystick.action.Enable();
    }

    void Update() {
        leftCooldown -= Time.deltaTime;
        rightCooldown -= Time.deltaTime;

        if (InputTriggerL.action.ReadValue<float>() >= 0.6f && leftCooldown <= 0f) {
            Instantiate(BulletPrefab, LeftHandRefference.position + LeftHandRefference.forward * SpawnOffset, LeftHandRefference.rotation);
            leftCooldown = AttackSpeed;
        }

        if (InputTriggerR.action.ReadValue<float>() >= 0.6f && rightCooldown <= 0f) {
            Instantiate(BulletPrefab, RightHandRefference.position + RightHandRefference.forward * SpawnOffset, RightHandRefference.rotation);
            rightCooldown = AttackSpeed;
        }

        if (InputGrabR.action.ReadValue<float>() >= 0.6f) {
            rb.angularVelocity = new Vector3(0, 0, 0);
            rb.linearVelocity = new Vector3(0, 0, 0);
            rb.gameObject.transform.position = new Vector3(-10, 10, 0);
        }

        MoveCharacterRigidbody();
    }

    void MoveCharacterCharacterController() {
        Vector2 joy = RightJoystick.action.ReadValue<Vector2>();

        if (joy.sqrMagnitude < 0.01f) return;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * joy.y + right * joy.x;

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void MoveCharacterRigidbody() {
        Vector2 joy = RightJoystick.action.ReadValue<Vector2>();
        if (joy.sqrMagnitude < 0.01f) return;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * joy.y + right * joy.x;
        moveDir.Normalize();

        Vector3 targetVelocity = moveDir * moveSpeed;
        Vector3 currentVelocity = rb.linearVelocity;

        rb.linearVelocity = new Vector3(
            targetVelocity.x,
            currentVelocity.y,
            targetVelocity.z
        );
    }

}