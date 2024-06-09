using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 8f;
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

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        // Horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Check if player touches the floor
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkDistance, groundLayer);

        // Check if player is touching a wall on the left or right
        isTouchingWallLeft = Physics2D.Raycast(wallCheckLeft.position, Vector2.left, checkDistance, wallLayer);
        isTouchingWallRight = Physics2D.Raycast(wallCheckRight.position, Vector2.right, checkDistance, wallLayer);

        // Jump if spacebar is pressed and the player is on the ground
        // isGrounded to prevent the player from jumping in the air
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {

            jumpRequest = true;
        }
    }

    private void FixedUpdate() {

        if (jumpRequest)  {
            Jump();
            jumpRequest = false;
        }
    }


    private void Jump()  {

        //Jump function. isGrounded is set to false to prevent double jumps
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        isGrounded = false; 
    }
   
}

/*
Next steps: Still working on moving enemies that can attack the player
*/