using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Action for starting the game
    public Action OnStartGame;

    // Action for loading a specific level
    public Action<string> OnLoadLevel;

    // When the button Start is pressed, invoke the action or load level 1 if no action is set
    public void StartGame()
    {
        if (OnStartGame != null)
        {
            OnStartGame.Invoke();
        }
        else
        {
            SceneManager.LoadScene("LevelSelectionMenu");
        }
    }

    // Method to be called when a level selection button is clicked
    public void LoadLevel(string levelName)
    {
        if (OnLoadLevel != null)
        {
            OnLoadLevel.Invoke(levelName);
        }
        else
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
