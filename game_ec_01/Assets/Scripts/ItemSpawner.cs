using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab; 
    public Vector2 spawnAreaMin; 
    public Vector2 spawnAreaMax; 
    public int maxItems; 
    public float raycastDistance = 50f;
    public LayerMask groundLayer; 
    public float overlapRadius = 0.4f; 

    private const int maxAttempts = 100;

    void Start() {
        SpawnItems();
    }

    //Spawn items with maximum 100 iterations for finding a valid position. 
    void SpawnItems() {

        int itemsSpawned = 0;
        int attempts = 0;

        while (itemsSpawned < maxItems && attempts < maxAttempts) {

            Vector3 randomPosition = GetRandomPositionWithinBounds();
            Debug.Log($"Random position before adjustment: {randomPosition}");

            if (TryGetGroundPosition(randomPosition, out Vector3 adjustedPosition)) {

                Debug.Log($"Adjusted position: {adjustedPosition}");

                if (IsValidPosition(adjustedPosition)) {

                    Instantiate(itemPrefab, adjustedPosition, Quaternion.identity);
                    Debug.Log("Spawned item at position: " + adjustedPosition);
                    itemsSpawned++;
                }
                else {
                    Debug.LogWarning($"Invalid position for item spawn at {adjustedPosition}, retrying...");
                }
            }
            else {
                Debug.LogWarning("Raycast did not hit ground, retrying...");
            }

            attempts++;
        }

        if (attempts >= maxAttempts) {
            Debug.LogWarning("Exceeded maximum attempts to spawn items. Some items may not have been spawned.");
        }
    }

    //Get random position 
    Vector3 GetRandomPositionWithinBounds() {
        float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector3 spawnPosition = new Vector3(x, y, 0);
        Debug.Log("Generated random position: " + spawnPosition);
        return spawnPosition;
    }

    //Method to get valid item position. Otherwise adjust position 
    bool TryGetGroundPosition(Vector3 position, out Vector3 adjustedPosition) {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, raycastDistance, groundLayer);
        Debug.DrawRay(position, Vector2.down * raycastDistance, Color.red, 1f);

        if (hit.collider != null) {
            adjustedPosition = new Vector3(position.x, hit.point.y + 0.5f, position.z);
            Debug.Log("Adjusted position to ground: " + adjustedPosition);
            return true;
        }

        Debug.LogWarning("Raycast did not hit any ground for position: " + position);
        adjustedPosition = position; // Return original position if no ground was hit
        return false;
    }

    //Check for valid position outside of the ground and inside the spawning area 
    bool IsValidPosition(Vector3 position) {
        if (position.x < spawnAreaMin.x || position.x > spawnAreaMax.x ||
            position.y < spawnAreaMin.y || position.y > spawnAreaMax.y) {

            Debug.LogWarning("Spawn position is outside spawn area: " + position);
            return false;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, overlapRadius, groundLayer);
        foreach (Collider2D collider in colliders) {
            if (collider.CompareTag("Ground")) {
                Debug.LogWarning("Invalid spawn position: " + position + " inside ground collider.");
                return false;
            }
        }

        return true;
    }

    //Visualize spawning area 
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Vector3 center = new Vector3((spawnAreaMin.x + spawnAreaMax.x) / 2, (spawnAreaMin.y + spawnAreaMax.y) / 2, 0);
        Vector3 size = new Vector3(spawnAreaMax.x - spawnAreaMin.x, spawnAreaMax.y - spawnAreaMin.y, 0);
        Gizmos.DrawWireCube(center, size);
    }
}
