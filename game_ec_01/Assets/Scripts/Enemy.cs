using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the enemy collides with the player, decrement the player's life points
        if (other.CompareTag("Player")) {
            GameManager.Instance.DecrementLife();
        }
    }
}
