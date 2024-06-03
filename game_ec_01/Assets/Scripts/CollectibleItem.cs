using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    //When the player collider got close enough to an item increase the item counter of the player. 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.CollectItem();
            }

            // Destroy item after it was collected. 
            Destroy(gameObject);
        }
    }
}

/* Script has to be extended to identify objects that cause damage. 
/  This could be the spike head in level 1. 
*/