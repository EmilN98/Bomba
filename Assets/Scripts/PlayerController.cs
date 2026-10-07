using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public GameObject bombPrefab;
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
        Vector3 bombPosition = transform.position + transform.forward;
        GameObject bombObject = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        Bomb bomb = bombObject.GetComponent<Bomb>();
        bomb.SetOwner(gameObject);
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