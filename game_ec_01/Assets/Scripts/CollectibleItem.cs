using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    // Tag for the weapon collectible
    public string weaponTag = "Weapon";

    //When the player collider got close enough to an item increase the item counter of the player. 
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            // Check if the collected item is a weapon
            if (gameObject.CompareTag(weaponTag)) {
                // Enable shooting ability for the player
                PlayerShooting playerShooting = other.GetComponent<PlayerShooting>();
                if (playerShooting != null) {
                    playerShooting.EnableShooting(); // Implement this method in your PlayerShooting script
                }
            } else {
                // If it's not a weapon, it's a regular collectible item
                PlayerController player = other.GetComponent<PlayerController>();
                if (player != null) {
                    player.CollectItem();
                }
            }

            // Destroy item after it was collected. 
            Destroy(gameObject);
        }
    }
}

