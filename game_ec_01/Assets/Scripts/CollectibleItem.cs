using UnityEngine;

public class CollectibleItem : MonoBehaviour {
    // Tag for the weapon collectible
    public string weaponTag = "Weapon";

    // Reference to the AudioSource component
    private AudioSource audioSource;

    void Start() {
        // Get the AudioSource component attached to the item
        audioSource = GetComponent<AudioSource>();
    }

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
            }
            else {
                // If it's not a weapon, it's a regular collectible item
                PlayerController player = other.GetComponent<PlayerController>();

                if (player != null) {

                    player.CollectItem();
                }
            }

            // Play the collection sound
            if (audioSource != null && audioSource.clip != null) {
                audioSource.Play();
            }

            // Make the item visually disappear
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;

            // Destroy the item after the audio clip finishes playing
            Destroy(gameObject, audioSource != null && audioSource.clip != null ? audioSource.clip.length : 0f);
        }
    }
}
