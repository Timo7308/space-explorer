using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {
    public GameObject dialoguePanel; 

public void DisplayInstruction() {

    if (dialoguePanel != null) {
       
       //Open dialogue panel
        dialoguePanel.SetActive(true);
    }
}

    // Close the dialogue panel
    public void CloseInstruction() {
        
        if (dialoguePanel != null) {
         
            dialoguePanel.SetActive(false); 
        }
    }    
}
