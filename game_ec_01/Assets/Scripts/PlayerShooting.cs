using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab of the projectile
    public Transform shootingPointRight; // Point where projectiles are spawned when shooting to the right
    public Transform shootingPointLeft; // Point where projectiles are spawned when shooting to the left
    public float shootingSpeed = 10f; // Speed of the projectile
    public float projectileLifetime = 3f; // Lifetime of the projectile

    private bool canShoot = false; // Flag to indicate if the player can shoot

    void Update()
    {
        if (canShoot && Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }
    }

    public void EnableShooting()
    {
        canShoot = true;
    }

  void Shoot()
{
    // Determine the shooting direction based on player's sprite orientation
    Transform currentShootingPoint;

    // Get the SpriteRenderer component of the player GameObject
    SpriteRenderer playerSpriteRenderer = GetComponent<SpriteRenderer>();

    if (playerSpriteRenderer.flipX)
    {
        // Player is facing left, use the left shooting point
        currentShootingPoint = shootingPointLeft;
    }
    else
    {
        // Default to right shooting point
        currentShootingPoint = shootingPointRight;
    }

    // Instantiate the projectile at the shooting point's position and rotation
    GameObject projectile = Instantiate(projectilePrefab, currentShootingPoint.position, currentShootingPoint.rotation);

    // Set the projectile as active to make it visible
    projectile.SetActive(true);

    // Get the Rigidbody2D component of the projectile
    Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

    // Calculate the shooting direction
    Vector2 shootingDirection = (playerSpriteRenderer.flipX) ? -currentShootingPoint.right : currentShootingPoint.right;

    // Apply a force to the projectile in the shooting direction
    rb.AddForce(shootingDirection * shootingSpeed, ForceMode2D.Impulse); // Apply impulse force

    // Destroy the projectile after a specified lifetime
    Destroy(projectile, projectileLifetime);
}

}
