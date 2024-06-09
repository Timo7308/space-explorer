using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the enemy collides with the player, decrement the player's life points
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.DecrementLife();
        }
    }
}

// Has to be extended for moving enemies that attack the player.
// Couldnt get it to work till now. 
// Right now only the spike heads in the game represent static enemies. 
