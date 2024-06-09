using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    // Text for collected items, images for life, game over screen, game won screen 
    public TMP_Text counterText; 
    public Image[] lifePointImages; 
    public GameObject gameOverPanel; 
    public GameObject gameWonPanel; 

    //fallThershold for maximum falling speed
    public float fallThreshold = 10f; 
    private Rigidbody2D rb; 

    //Track falling speed 
    private float maxFallSpeed; 
    private PlayerStats playerStats; 

    void Start() {
        //Player starts with 3 lifes and has to collect 4 items 
        rb = GetComponent<Rigidbody2D>();
        playerStats = new PlayerStats(3, 4); 
        UpdateCounterText();
        UpdateLifePointImages();
    }

    void FixedUpdate() {
        //Check falling speed 
        if (rb.velocity.y < maxFallSpeed) {
            maxFallSpeed = rb.velocity.y;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // Check if player touched the ground
        if (collision.gameObject.CompareTag("Ground")) {
            // If falling speed is to fast reduce life by one 
            if (maxFallSpeed <= -fallThreshold) {
                TakeDamage(1);
            }

            // Reset falling speed 
            maxFallSpeed = 0f;
        }
    }

    // If player collected all items and collided with the goal object the game is won
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Goal") && playerStats.itemCount >= playerStats.maxItemCount) {
            GameWon();
        }
        // If player collides with an enemy reduce life by one
        else if (other.gameObject.CompareTag("Enemy")) {
            TakeDamage(1);
        }
    }

    //If all life points are gone the game is over
    void TakeDamage(int damageAmount) {
        playerStats.TakeDamage(damageAmount);
        UpdateLifePointImages();

        if (playerStats.IsDead()) {
            GameOver();
        }
    }

    // If player collects an item, increase counter by one
    public void CollectItem() {
        playerStats.CollectItem();
        UpdateCounterText();
    }

    // Display number of collected items
    private void UpdateCounterText() {
        if (counterText != null) {
            counterText.text = "Minerals: " + playerStats.itemCount.ToString() + " / " + playerStats.maxItemCount.ToString();
        }
    }

    // Update the UI to display life point images
    private void UpdateLifePointImages() {
        for (int i = 0; i < lifePointImages.Length; i++) {
            lifePointImages[i].enabled = i < playerStats.currentLifePoints;
        }
    }

    void GameOver() {

        // Activate the Game Over panel
        if (gameOverPanel != null) {
            gameOverPanel.SetActive(true);
        }
    }

    void GameWon() {

        // Activate the Game Won panel
        if (gameWonPanel != null) {
            gameWonPanel.SetActive(true);
        }
    }

    // Method to restart the game
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}