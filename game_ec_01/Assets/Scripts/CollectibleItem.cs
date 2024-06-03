using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Increase the player's item counter
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.CollectItem();
            }

            // Destroy the item GameObject
            Destroy(gameObject);
        }
    }
}
