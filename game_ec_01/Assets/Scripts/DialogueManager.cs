using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {
  //  public DialogueInstruction dialogueInstruction; 
  //  public TMP_Text Text;
    public GameObject dialoguePanel; 


public void DisplayInstruction() {

    if (dialoguePanel != null) {
       
        // Update the text of the UI Text component with the instruction text from the Scriptable Object
     //   Text.text = dialogueInstruction.instructionText;

        // Activate the dialogue panel to show the window
        dialoguePanel.SetActive(true);
    }
}

    // Close the dialogue panel
    public void CloseInstruction() {
        // Deactivate the UI Text component and the panel GameObject to hide the window
        if (dialoguePanel != null)
        {
           // Text.gameObject.SetActive(false); 
            dialoguePanel.SetActive(false); 
        }
    }    
}
