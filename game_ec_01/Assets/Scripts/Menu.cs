using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // This method will be called when the button is clicked
    public void StartGame()
    {
        // Load the first level (assuming it's named "Level1")
        SceneManager.LoadScene("Level1");
    }
}
