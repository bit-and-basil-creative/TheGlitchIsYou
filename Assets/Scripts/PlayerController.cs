using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float airControlMultiplier = 1f;
    [SerializeField] private float maxFallSpeed = -10f;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool invertControls = false;

    private Rigidbody2D characterRigidBody;
    private bool isGrounded = true;
    private float moveHorizontal;
    private Vector2 currentVelocity;

    // Exposed properties for hex manipulation
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public float AirControlMultiplier { get => airControlMultiplier; set => airControlMultiplier = value; }
    public float MaxFallSpeed { get => maxFallSpeed; set => maxFallSpeed = value; }
    public bool CanMove { get => canMove; set => canMove = value; }
    public bool InvertControls { get => invertControls; set => invertControls = value; }

    // Start is called before the first frame update
    void Start()
    {
        characterRigidBody = GetComponent<Rigidbody2D>(); //get a reference to the player character rigidbody component
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxis("Horizontal");

        moveHorizontal = invertControls ? -input : input; //start with regular control inputs

        //if player presses the spacebar call the jump methd
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        //flip character sprite based on movement direction
        if (moveHorizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveHorizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FixedUpdate()
    {
        currentVelocity = characterRigidBody.velocity;

        if (canMove)
        {
            float controlMultiplier = isGrounded ? 1f : airControlMultiplier;
            characterRigidBody.velocity = new Vector2(moveHorizontal * movementSpeed * controlMultiplier, currentVelocity.y);

            if (characterRigidBody.velocity.y < maxFallSpeed)
            {
                characterRigidBody.velocity = new Vector2(characterRigidBody.velocity.x, maxFallSpeed);
            }
        }
    }

    private void Jump()
    {
        characterRigidBody.velocity = new Vector2(characterRigidBody.velocity.x, jumpForce);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
