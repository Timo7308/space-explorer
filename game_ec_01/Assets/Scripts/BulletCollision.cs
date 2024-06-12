using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided GameObject has the "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            // Destroy the enemy GameObject
            Destroy(other.gameObject);
            // Destroy the bullet GameObject
            Destroy(gameObject);
        }
    }
}
