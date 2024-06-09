using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Action for starting the game
    public Action OnStartGame;

    // When the button Start is pressed, invoke the action or load level 1 if no action is set
    public void StartGame() {

        if (OnStartGame != null) {
            OnStartGame.Invoke();
        }
        else {
            SceneManager.LoadScene("Level1");
        }
    }
}
