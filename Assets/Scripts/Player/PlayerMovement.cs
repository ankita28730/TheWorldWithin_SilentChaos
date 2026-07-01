using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    public PlayerControls control;
    private Vector2 moveInput;
    public PlayerInteraction playerInteraction;
    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerInteraction = GetComponent<PlayerInteraction>();
        control = new PlayerControls();
        control.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        control.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        control.Player.Interact.performed += ctx => playerInteraction.TryInteract();
    }
    private void OnEnable() {
        control.Enable();
    }
    private void OnDisable() {
        control.Disable();
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        // Debug.Log("move input = " + moveInput);
    }

}
