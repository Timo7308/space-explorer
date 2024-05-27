using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheckLeft;
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float checkDistance = 0.25f;

    private Rigidbody2D body;
    private bool isGrounded;
    private bool isTouchingWallLeft;
    private bool isTouchingWallRight;
    private bool jumpRequest;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkDistance, groundLayer);

        // Check if the player is touching a wall on the left or right
        isTouchingWallLeft = Physics2D.Raycast(wallCheckLeft.position, Vector2.left, checkDistance, wallLayer);
        isTouchingWallRight = Physics2D.Raycast(wallCheckRight.position, Vector2.right, checkDistance, wallLayer);

        // Jump if space is pressed and the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequest = true;
        }
    }

    private void FixedUpdate()
    {
        if (jumpRequest)
        {
            Jump();
            jumpRequest = false;
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        isGrounded = false; // Immediately set isGrounded to false to prevent double jumps
    }

    // Other collision detection methods...
}