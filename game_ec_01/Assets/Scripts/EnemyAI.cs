using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public float chaseSpeed = 4f; 
    public Transform leftBoundary; 
    public Transform rightBoundary; 
    public Transform player;
    public float detectionRange = 10f;
    public float stopChaseRange = 0.5f; // Minimum distance to stop chasing
    public LayerMask playerLayer;
    public Animator animator;

    private bool movingRight = true;
    private Rigidbody2D rb;
    private bool isChasingPlayer = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isMoving", false); 
    }

    void FixedUpdate()
    {
        CheckForPlayer();

        if (isChasingPlayer) {
            ChasePlayer();
        }
        else {
            Patrol();
        }
    }

    void Patrol() {
        animator.SetBool("isMoving", true);

        if (movingRight) {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (transform.position.x >= rightBoundary.position.x) {
                movingRight = false;
                Flip();
            }
        }
        else {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (transform.position.x <= leftBoundary.position.x) {
                movingRight = true;
                Flip();
            }
        }
    }

    void CheckForPlayer() {
        if (player == null) {
            Debug.LogError("Player not assigned in EnemyAI script.");
            return;
        }

        float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x);

        if (distanceToPlayer <= detectionRange) {
            Vector2 direction = (player.position.x > transform.position.x) ? Vector2.right : Vector2.left;

            // Offset the raycast origin to avoid hitting the enemy itself
            Vector2 raycastOrigin = new Vector2(transform.position.x, transform.position.y);

            RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, direction, detectionRange, playerLayer);

            Debug.DrawRay(raycastOrigin, direction * detectionRange, Color.green);

            if (hit.collider != null && hit.collider.transform == player) {
                Debug.Log("Player detected!");
                isChasingPlayer = true;
            }
            else {
                isChasingPlayer = false;
            }
        }
        else {
            isChasingPlayer = false;
        }
    }

    void ChasePlayer() {
        animator.SetBool("isMoving", true);

        float direction = player.position.x - transform.position.x;

        if (Mathf.Abs(direction) <= stopChaseRange) {
            // Stop moving if the player is too close
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (player.position.x >= leftBoundary.position.x && player.position.x <= rightBoundary.position.x) {
            rb.velocity = new Vector2(Mathf.Sign(direction) * chaseSpeed, rb.velocity.y);

            if ((direction < 0 && movingRight) || (direction > 0 && !movingRight)) {
                movingRight = !movingRight;
                Flip();
            }
        }
        else {
            isChasingPlayer = false;
        }

        if (Mathf.Abs(direction) > detectionRange)  {
            isChasingPlayer = false;
        }
    }

    void Flip() {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
