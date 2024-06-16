using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public TMP_Text counterText;
    public Image[] lifePointImages;
    public GameObject gameOverPanel;
    public GameObject gameWonPanel;

    public float fallThreshold = 10f;
    public AudioClip enemyCollisionSound; // Sound to play when colliding with an enemy
    private AudioSource audioSource; // Reference to the AudioSource component on the player

    private Rigidbody2D rb;
    private float maxFallSpeed;
    private PlayerStats playerStats;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerStats = new PlayerStats(3, 4);

        // Get the AudioSource component from the player GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if not already present
        }

        playerStats.OnLifeChanged += UpdateLifePointImages;
        playerStats.OnItemCollected += UpdateCounterText;
        playerStats.OnGameOver += GameOver;
        playerStats.OnGameWon += GameWon;

        UpdateCounterText(playerStats.itemCount);
        UpdateLifePointImages(playerStats.currentLifePoints);
    }

    void FixedUpdate() {
        if (rb.velocity.y < maxFallSpeed) {
            maxFallSpeed = rb.velocity.y;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collision entered with: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Ground")) {
            if (maxFallSpeed <= -fallThreshold) {
                playerStats.TakeDamage(1);
                PlayEnemyCollisionSound();
            }
            maxFallSpeed = 0f;
        } else if (collision.gameObject.CompareTag("Enemy")) {
            playerStats.TakeDamage(1);
            PlayEnemyCollisionSound();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Trigger entered with: " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Goal")) {
            Debug.Log("Player has touched the goal!");
            playerStats.TouchGoal();
        } else if (other.gameObject.CompareTag("Enemy")) {
            playerStats.TakeDamage(1);
            PlayEnemyCollisionSound();
        }
    }

    public void CollectItem() {
        playerStats.CollectItem();
    }

    private void UpdateCounterText(int itemCount) {
        if (counterText != null) {
            counterText.text = "Minerals: " + itemCount.ToString() + " / " + playerStats.maxItemCount.ToString();
        }
    }

    private void UpdateLifePointImages(int currentLifePoints) {
        for (int i = 0; i < lifePointImages.Length; i++) {
            lifePointImages[i].enabled = i < currentLifePoints;
        }
    }

    void GameOver() {
        if (gameOverPanel != null) {
            gameOverPanel.SetActive(true);
        }
    }

    void GameWon() {
        if (gameWonPanel != null) {
            gameWonPanel.SetActive(true);
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   void PlayEnemyCollisionSound() {
    if (audioSource != null && enemyCollisionSound != null) {
        audioSource.volume = 1.0f; // Adjust volume (1.0f is max volume)
        audioSource.PlayOneShot(enemyCollisionSound);
    }
}

}
