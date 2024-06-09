using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

//Game states
public enum GameState {
    Playing,
    GameOver,
    GameWon
}

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public GameState currentState { get; private set; }
    public DialogueManager dialogueManager; 
    public GameObject gameOverPanel; 
    public GameObject gameWonPanel; 
    public PlayerController playerController; 
    public PlayerStats playerStats;

    public Menu menu;

    public int playerLives = 3; 
    public Image[] livesImages; 
    public Sprite fullHeartSprite; 
    public Sprite emptyHeartSprite; 
   

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    void Start() {
       //Delay for showing the instructions screen
        StartCoroutine(DisplayInstructionWithDelay(2f));

        // Set initial game state
        SetGameState(GameState.Playing);

        if (menu != null) {
            menu.OnStartGame = CustomStartGameAction;
        }

    }

    private IEnumerator DisplayInstructionWithDelay(float delay) {
        yield return new WaitForSeconds(delay);
        
        // Call the DisplayInstruction method of the DialogueManager script
        if (dialogueManager != null) {
            dialogueManager.DisplayInstruction();
        }
        else {
            Debug.LogWarning("DialogueManager reference not assigned.");
        }
    }

    public void SetGameState(GameState newState) {
        currentState = newState;
        HandleGameStateChanged(newState);
    }


    //When game over or game won disable controls for player
    //Currently not working 
    private void HandleGameStateChanged(GameState newState) {
        if (newState == GameState.GameOver) {
            if (gameOverPanel != null) {
                gameOverPanel.SetActive(true);
                DisablePlayerControls();
            }
        }
        else if (newState == GameState.GameWon) {
            if (gameWonPanel != null) {
                gameWonPanel.SetActive(true);
                DisablePlayerControls();
            }
        }
        //When game state is playing enable controls
        else if (newState == GameState.Playing) {
            EnablePlayerControls();
        }  
    }

    private void DisablePlayerControls() {
        if (playerController != null) {
            playerController.enabled = false;
        }
    }

    private void EnablePlayerControls() {
        if (playerController != null) {
            playerController.enabled = true;
        }
    }

    public void DecrementLife() {
        playerLives--;
        UpdateLivesUI();

        if (playerLives <= 0) {
           
            Debug.Log("Game Over!");
        }
    }

    // Display full and empty life images. Not working yet
    private void UpdateLivesUI() {
        for (int i = 0; i < livesImages.Length; i++) {
            if (i < playerLives) {
                // Display full heart sprite for remaining lives
                livesImages[i].sprite = fullHeartSprite;
            }
            else  {
                // Display empty heart sprite for lost lives
                livesImages[i].sprite = emptyHeartSprite;
            }
        }
    }

    //Game states
    public void GameOver() {
        SetGameState(GameState.GameOver);
    }

    public void GameWon() {
    // Check if the player has collected all four items
    if (playerStats.itemCount >= playerStats.maxItemCount) {
        Debug.Log("All items collected.");
        // Check if the player is touching the goal object
       
            SetGameState(GameState.GameWon);
            // Deactivate the game won panel before returning to the menu
          
            ReturnToMenu();
        }
       
    }
   





    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("Menu");
    }
     private void CustomStartGameAction() {
        Debug.Log("Custom start game action triggered!");
        SceneManager.LoadScene("Level1");
    }
}
