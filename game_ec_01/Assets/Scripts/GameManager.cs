using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;

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
    public StoryManager storyManager;
    public GameObject gameOverPanel; 
    public GameObject gameWonPanel; 
    public PlayerController playerController; 
    public PlayerStats playerStats;
    public AudioSource backgroundMusicAudioSource; 

  

    public int playerLives = 3; 
    public Image[] livesImages; 
    public Sprite fullHeartSprite; 
    public Sprite emptyHeartSprite;

    // Timer variables
    public float totalTimerDuration = 60f; 
    private float currentTimer;
    private bool timerRunning = true; // Flag to control the timer

    // UI Text to display timer
    public TMP_Text timerText;

    void Awake() {
        // Initialize current timer
        currentTimer = totalTimerDuration;

        // Initialize background music
        if (backgroundMusicAudioSource == null) {
            backgroundMusicAudioSource = GetComponent<AudioSource>();
            if (backgroundMusicAudioSource == null) {
                backgroundMusicAudioSource = gameObject.AddComponent<AudioSource>();
            }
        }
         
        if (backgroundMusicAudioSource.clip == null) {
            AudioClip bgMusicClip = Resources.Load<AudioClip>("path_to_your_audio_clip");
            if (bgMusicClip != null) {
                backgroundMusicAudioSource.clip = bgMusicClip;
            } 
        }

        DontDestroyOnLoad(gameObject);
        PlayBackgroundMusic();
    }

    void Start() {
        StartCoroutine(DisplayInstructionWithDelay(2f));
        SetGameState(GameState.Playing);

        // Initialize timer text if it's not set in the Inspector
        if (timerText == null) {
            Debug.LogError("Timer Text object reference is not set!");
        } 
        else {
            UpdateTimerText();
        }
    }

    //Display instruction panel at the beginning of level with delay 
    IEnumerator DisplayInstructionWithDelay(float delay) {
        yield return new WaitForSeconds(delay);

        if (storyManager != null) {
            storyManager.DisplayInstruction();
        }
         if (dialogueManager != null) {
            dialogueManager.DisplayInstruction();
        }

    }

    //Display result on Game Won panel with delay 
    IEnumerator DisplayResultInstructionWithDelay(float delay) {
        yield return new WaitForSeconds(delay);

        if (storyManager != null) {
            storyManager.DisplayResultInstruction();
           
        }
    }

    //Manage conditions for opening GameWon or Game Over panel 
    void Update() {
        if (currentState == GameState.Playing && timerRunning) {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0f) {
                GameOver();
            }
            UpdateTimerText(); // Update timer display each frame
        }

        // Check if either game over or game won panel is active
        if (gameOverPanel != null && gameOverPanel.activeSelf) {
            StopBackgroundMusic();
        } 
        else if (gameWonPanel != null && gameWonPanel.activeSelf) {
            StopBackgroundMusic();
            StopTimer();
            StartCoroutine(DisplayResultInstructionWithDelay(5f));
        }
    }

    //Handle game states 
    public void SetGameState(GameState newState) {
        currentState = newState;
        HandleGameStateChanged(newState);
        Debug.Log("Game State Changed to: " + newState); 
    }

    public void HandleGameStateChanged(GameState newState) {
        if (newState == GameState.GameOver) {
            if (gameOverPanel != null) {
                gameOverPanel.SetActive(true);
                StopBackgroundMusic();
                DisablePlayerControls();
                StopTimer();
            }
        } 
        else if (newState == GameState.GameWon) {
            if (gameWonPanel != null) {
                gameWonPanel.SetActive(true);
                StopBackgroundMusic();
                DisablePlayerControls();
                StopTimer();
            }
        } 
        else if (newState == GameState.Playing) {
            EnablePlayerControls();
            PlayBackgroundMusic();
            StartTimer();
        }
    }

    public void StopTimer() {
        timerRunning = false;
    }

    public void StartTimer() {
        timerRunning = true;
    }

    public void DisablePlayerControls() {
        if (playerController != null) {
            playerController.enabled = false;
        }
    }

    public void EnablePlayerControls() {
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

    public void UpdateLivesUI() {
        for (int i = 0; i < livesImages.Length; i++) {
            if (i < playerLives) {
                livesImages[i].sprite = fullHeartSprite;
            } 
            else {
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

    public void PlayBackgroundMusic() {
        if (backgroundMusicAudioSource != null && !backgroundMusicAudioSource.isPlaying) {
            backgroundMusicAudioSource.Play();
        }
    }

    public void StopBackgroundMusic() {
        if (backgroundMusicAudioSource != null && backgroundMusicAudioSource.isPlaying) {
            backgroundMusicAudioSource.Stop();
        }
    }

    public string GetFormattedTimeRemaining() {
        int minutes = Mathf.FloorToInt(currentTimer / 60);
        int seconds = Mathf.FloorToInt(currentTimer % 60);
        return string.Format("{0}:{1:00}", minutes, seconds);
    }

    // Update timer text UI element
    private void UpdateTimerText() {
        if (timerText != null) {
            timerText.text = "Time: " + GetFormattedTimeRemaining();
        }
    }
}
