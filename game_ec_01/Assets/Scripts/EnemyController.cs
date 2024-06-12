using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public Transform leftGroundCheck;
    public Transform rightGroundCheck;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    public float directionChangeCooldown = 2.0f;

    private bool movingRight = true;
    private Rigidbody2D rb;
    private float directionChangeTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
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
                movingRight = false;
                Flip();
                directionChangeTimer = 0f; // Reset the timer
            }
            else if (!movingRight && !isLeftGrounded)
            {
                movingRight = true;
                Flip();
                directionChangeTimer = 0f; // Reset the timer
            }
        }

        Move();
    }

    void Move()
    {
        rb.velocity = new Vector2(movingRight ? speed : -speed, rb.velocity.y);
    }

    void Flip()
    {
        // Flip the enemy sprite by changing its orientation
        Vector3 theScale = transform.localEulerAngles;
        theScale.y += 180;
        transform.localEulerAngles = theScale;
    }
}
