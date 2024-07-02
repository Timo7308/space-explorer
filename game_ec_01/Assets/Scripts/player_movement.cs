using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public Animator animator;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheckLeft;
    [SerializeField] private Transform wallCheckRight;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float checkDistance = 0.25f;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    private bool isTouchingWallLeft;
    private bool isTouchingWallRight;
    private bool jumpRequest;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        // Horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        // Flip sprite horizontally if moving left
        if (horizontalInput < 0) {
            spriteRenderer.flipX = true;
        }
        // Flip sprite back to original orientation if moving right
        else if (horizontalInput > 0) {
            spriteRenderer.flipX = false;
        }

        // Check if player touches the floor
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkDistance, groundLayer);

        // Set animator parameter based on isGrounded state
        animator.SetBool("IsJumping", !isGrounded);

        // Check if player is touching a wall on the left or right
        isTouchingWallLeft = Physics2D.Raycast(wallCheckLeft.position, Vector2.left, checkDistance, wallLayer);
        isTouchingWallRight = Physics2D.Raycast(wallCheckRight.position, Vector2.right, checkDistance, wallLayer);

        // Jump if spacebar is pressed and the player is on the ground
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequest = true;
            animator.SetBool("IsJumping", true); // Set IsJumping to true when jump is initiated
        }
    }

    private void FixedUpdate() {
        if (jumpRequest) {
            Jump();
            jumpRequest = false;
        }
    }

    private void Jump() {
        // Jump function
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        // Check if the player lands on the moving platform
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        // Reset parent when the player leaves the moving platform
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }
}
