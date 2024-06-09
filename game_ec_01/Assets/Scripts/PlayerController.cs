using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public TMP_Text counterText;
    public Image[] lifePointImages;
    public GameObject gameOverPanel;
    public GameObject gameWonPanel;


    //Maximum falling speed
    public float fallThreshold = 10f;
    private Rigidbody2D rb;

    private float maxFallSpeed;
    private PlayerStats playerStats;

    void Start() {

        //Player starts with three lifes and can collect four items. 
        rb = GetComponent<Rigidbody2D>();
        playerStats = new PlayerStats(3, 4);

        // Subscribe to events
        playerStats.OnLifeChanged += UpdateLifePointImages;
        playerStats.OnItemCollected += UpdateCounterText;
        playerStats.OnGameOver += GameOver;
        playerStats.OnGameWon += GameWon;

        UpdateCounterText(playerStats.itemCount);
        UpdateLifePointImages(playerStats.currentLifePoints);
    }

    void FixedUpdate()  {
        if (rb.velocity.y < maxFallSpeed) {
            maxFallSpeed = rb.velocity.y;
        }
    }

    //When the player touches the ground while falling too fast
    //reduce life points by one and then reset falling speed
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            if (maxFallSpeed <= -fallThreshold) {
                playerStats.TakeDamage(1);
            }

            maxFallSpeed = 0f;
        }
    }

    //Collect item and take damage when tag was found
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal") && playerStats.itemCount >= playerStats.maxItemCount)
        {
            playerStats.CollectItem();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            playerStats.TakeDamage(1);
        }
    }

    public void CollectItem() {
        playerStats.CollectItem();
    }

    // Update item counter 
    private void UpdateCounterText(int itemCount) {
        if (counterText != null) {
            counterText.text = "Minerals: " + itemCount.ToString() + " / " + playerStats.maxItemCount.ToString();
        }
    }

    //Update health bar
    private void UpdateLifePointImages(int currentLifePoints) {
        for (int i = 0; i < lifePointImages.Length; i++) {
            lifePointImages[i].enabled = i < currentLifePoints;
        }
    }

    //Game Over state
    void GameOver() {
        if (gameOverPanel != null) {
            gameOverPanel.SetActive(true);
        }
    }

    //Game Won state
    void GameWon() {
        if (gameWonPanel != null) {
            gameWonPanel.SetActive(true);
        }
    }

    //Restart Game 
    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
