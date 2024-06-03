using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //When the button Start was pressed load level 1. 
    public void StartGame()
    {
       
        SceneManager.LoadScene("Level1");
    }
}

/* Not the final structure of the game. Will be extended with another level
/  and a sub menu to choose the next level. 
*/