using UnityEngine;
using UnityEngine.InputSystem;

// https://www.youtube.com/watch?v=Yjee_e4fICc
public class TestingInputSystem : MonoBehaviour
{
    PlayerInputActions input;
    Vector2 mousePos;
    Vector2 inputVector;

    [SerializeField]
    private float movementSpeed = 6f;

    [SerializeField]
    private Camera playerCamera;

    private void Awake()
    {
        input = new PlayerInputActions();

        input.Player.Enable();

        input.Player.Mouse.performed += onMouse;
        input.Player.Movement.performed += onMovement;
        input.Player.Movement.canceled += onMovement;
    }

    private void FixedUpdate()
    {
        Vector3 targetVector = new Vector3(inputVector.x, 0, inputVector.y);
        var speed = movementSpeed * Time.deltaTime;

        // controller.Move(direction * speed * Time.deltaTime);
        
        targetVector = Quaternion.Euler(0, playerCamera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;

    }

    private void onMovement(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }

    private void onMouse(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }
}
