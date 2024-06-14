using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public Transform leftGroundCheck;
    public Transform rightGroundCheck;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    public float directionChangeCooldown = 4.0f;
    public float pauseDuration = 15.0f; // Duration to pause before changing direction

    private bool movingRight = true;
    private Rigidbody2D rb;
    private float directionChangeTimer = 0f;
    private bool isPausing = false;
    private float pauseTimer = 0f;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false); // Ensure Idle state at start
    }

    void FixedUpdate()
    {
        if (isPausing)
        {
            HandlePause();
        }
        else
        {
            directionChangeTimer += Time.deltaTime;

            bool isLeftGrounded = Physics2D.Raycast(leftGroundCheck.position, Vector2.down, groundCheckDistance, groundLayer);
            bool isRightGrounded = Physics2D.Raycast(rightGroundCheck.position, Vector2.down, groundCheckDistance, groundLayer);

            Debug.DrawRay(leftGroundCheck.position, Vector2.down * groundCheckDistance, Color.red);
            Debug.DrawRay(rightGroundCheck.position, Vector2.down * groundCheckDistance, Color.red);

            if (directionChangeTimer >= directionChangeCooldown)
            {
                if (movingRight && !isRightGrounded)
                {
                    StartPause();
                }
                else if (!movingRight && !isLeftGrounded)
                {
                    StartPause();
                }
            }

            if (!isPausing)
            {
                Move();
                animator.SetBool("isMoving", true);
            }
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(movingRight ? speed : -speed, rb.velocity.y);
    }

    void Flip()
    {
        // Flip the enemy sprite by changing its orientation
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void StartPause()
    {
        isPausing = true;
        pauseTimer = pauseDuration;
        rb.velocity = Vector2.zero; // Stop movement during pause
        animator.SetBool("isMoving", false);
    }

    void HandlePause()
    {
        pauseTimer -= Time.deltaTime;

        // Check if pauseTimer is less than or equal to 0
        if (pauseTimer <= 0f)
        {
            isPausing = false;
            directionChangeTimer = 0f; // Reset the direction change timer after pausing
            movingRight = !movingRight; // Change direction after pausing
            Flip();
            animator.SetBool("isMoving", true); // Set isMoving to true when pause ends
        }
        else
        {
            animator.SetBool("isMoving", false); // Set isMoving to false during pause
        }
    }
}
