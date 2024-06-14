using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour {
    public DialogueInstruction dialogueInstruction; 
    public TMP_Text Text;
    public GameObject dialoguePanel; 

  
public void DisplayInstruction() {

    if (dialogueInstruction != null && Text != null && dialoguePanel != null) {
       
        Debug.Log("Displaying instruction text: " + dialogueInstruction.instructionText);

        // Update the text of the UI Text component with the instruction text from the Scriptable Object
        Text.text = dialogueInstruction.instructionText;

        // Activate the dialogue panel to show the window
        dialoguePanel.SetActive(true);
    }
    else {
       
        Debug.LogWarning("Dialogue instruction, UI Text component, or panel GameObject not assigned.");
    }
}


    // Close the dialogue panel
    public void CloseInstruction() {
        // Deactivate the UI Text component and the panel GameObject to hide the window
        if (Text != null && Text.gameObject != null && dialoguePanel != null)
        {
            Text.gameObject.SetActive(false); 
            dialoguePanel.SetActive(false); 
            Debug.Log("Dialogue panel closed.");
        }
        else {
            Debug.LogWarning("UI Text component or panel GameObject not assigned.");
        }
    }
}
