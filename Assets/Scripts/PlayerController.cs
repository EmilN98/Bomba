using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public PlayerInput playerInput;
    public float movementSpeed = 5f;
    private Vector3 moveInput;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
    }
    public void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        moveInput = new Vector3(-input.x, 0, -input.y);
    }
    public void OnBomb()
    {
        Debug.Log("Bomb");
    }
    void Update()
    {
        if (moveInput != Vector3.zero)
        {
            transform.forward = moveInput;
            characterController.Move(moveInput * movementSpeed * Time.deltaTime);
        }
    }
}