using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Action for starting the game
    public Action OnStartGame;

    // Action for loading a specific level
    public Action<string> OnLoadLevel;

    // Reference to the pause panel
    public GameObject pausePanel;

    // References to the buttons in the pause panel
    public Button quitButton;
    public Button continueButton;

    // Reference to the panel to open
    public GameObject panelToOpen;

    private bool isPaused = false;

    void Start() {
        // Assign the button click listeners
        if (quitButton != null) {

            quitButton.onClick.AddListener(QuitToLevelSelection);
        }
        if (continueButton != null) {

            continueButton.onClick.AddListener(ResumeGame);
        }
    }

    // When the button Start is pressed, invoke the action or load level selection menu if no action is set
    public void StartGame() {
        if (OnStartGame != null) {
            OnStartGame.Invoke();
        }
        else {
            SceneManager.LoadScene("LevelSelectionMenu");
        }
    }

    // Method to be called when a level selection button is clicked
    public void LoadLevel() {
        string levelName = "Level1"; // Specify the level name here
        Debug.Log("LoadLevel called with levelName: " + levelName); // Add debug log

        if (!string.IsNullOrEmpty(levelName)) {
            if (OnLoadLevel != null) {
                OnLoadLevel.Invoke(levelName);
            }
            else {
                SceneManager.LoadScene(levelName);
            }
        }
        else {
            Debug.LogError("Cannot load scene: Invalid scene name (empty string)");
        }
    }
    public void LoadLevel3() {
        string levelName = "Level3"; // Specify the level name here
        Debug.Log("LoadLevel called with levelName: " + levelName); // Add debug log

        if (!string.IsNullOrEmpty(levelName)) {
            if (OnLoadLevel != null) {
                OnLoadLevel.Invoke(levelName);
            }
            else {
                SceneManager.LoadScene(levelName);
            }
        }
        else {
            Debug.LogError("Cannot load scene: Invalid scene name (empty string)");
        }
    }









    // Method to handle the pause button click
    public void PauseGame()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            ShowPausePanel();
        }
    }

    // Show the pause panel and pause the game
    private void ShowPausePanel()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    // Hide the pause panel and resume the game
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    // Quit to the level selection menu
    public void QuitToLevelSelection()
    {
        Time.timeScale = 1f; // Ensure time scale is reset to normal
        SceneManager.LoadScene("LevelSelectionMenu");
    }

    // Method to go back to the main menu from level selection
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Method to open the specified panel
    public void OpenPanel()
    {
        if (panelToOpen != null)
        {
            panelToOpen.SetActive(true);
        }
    }
}
