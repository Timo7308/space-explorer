using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

// Enum for game states
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
    public AudioSource backgroundMusicAudioSource; // Attached AudioSource for background music

    public int playerLives = 3; 
    public Image[] livesImages; 
    public Sprite fullHeartSprite; 
    public Sprite emptyHeartSprite; 

   void Awake() {
    // Ensure there's an AudioSource component on this GameObject
    if (backgroundMusicAudioSource == null) {
        backgroundMusicAudioSource = GetComponent<AudioSource>();
        if (backgroundMusicAudioSource == null) {
            backgroundMusicAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Assign the audio clip if it's not assigned yet
    if (backgroundMusicAudioSource.clip == null) {
        // Replace "path_to_your_audio_clip" with the actual path to your audio clip
        AudioClip bgMusicClip = Resources.Load<AudioClip>("path_to_your_audio_clip");
        if (bgMusicClip != null) {
            backgroundMusicAudioSource.clip = bgMusicClip;
        } else {
            Debug.LogError("Background music audio clip not found or assigned.");
        }
    }

    // Ensure this GameManager persists across scenes
    DontDestroyOnLoad(gameObject);

    // Start playing background music if the game state allows
    PlayBackgroundMusic();
}


    void Start() {
        // Delay for showing the instructions screen
        StartCoroutine(DisplayInstructionWithDelay(2f));

        // Set initial game state
        SetGameState(GameState.Playing);
    }

    IEnumerator DisplayInstructionWithDelay(float delay) {
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

    void HandleGameStateChanged(GameState newState) {
        if (newState == GameState.GameOver) {
            if (gameOverPanel != null) {
                gameOverPanel.SetActive(true);
                StopBackgroundMusic();
                DisablePlayerControls();
            }
        }
        else if (newState == GameState.GameWon) {
            if (gameWonPanel != null) {
                gameWonPanel.SetActive(true);
                StopBackgroundMusic();
                DisablePlayerControls();
            }
        }
        else if (newState == GameState.Playing) {
            EnablePlayerControls();
            PlayBackgroundMusic();
        }  
    }

    void DisablePlayerControls() {
        if (playerController != null) {
            playerController.enabled = false;
        }
    }

    void EnablePlayerControls() {
        if (playerController != null) {
            playerController.enabled = true;
        }
    }

    public void DecrementLife() {
        playerLives--;
        UpdateLivesUI();

        if (playerLives <= 0) {
            GameOver();
        }
    }

    void UpdateLivesUI() {
        for (int i = 0; i < livesImages.Length; i++) {
            if (i < playerLives) {
                livesImages[i].sprite = fullHeartSprite;
            }
            else  {
                livesImages[i].sprite = emptyHeartSprite;
            }
        }
    }

    public void GameOver() {
        SetGameState(GameState.GameOver);
    }

    public void GameWon() {
        if (playerStats.itemCount >= playerStats.maxItemCount) {
            SetGameState(GameState.GameWon);
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("Menu");
    }

  void Update() {
        // Check if either game over or game won panel is active
        if (gameOverPanel != null && gameOverPanel.activeSelf) {
            StopBackgroundMusic();
        }
        else if (gameWonPanel != null && gameWonPanel.activeSelf) {
            StopBackgroundMusic();
        }
    }

    void PlayBackgroundMusic() {
        if (backgroundMusicAudioSource != null && !backgroundMusicAudioSource.isPlaying) {
            backgroundMusicAudioSource.Play();
        }
    }

    void StopBackgroundMusic() {
        if (backgroundMusicAudioSource != null && backgroundMusicAudioSource.isPlaying) {
            backgroundMusicAudioSource.Stop();
        }
    }
}
