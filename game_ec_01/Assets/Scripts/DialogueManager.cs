using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public DialogueInstruction dialogueInstruction; 
    public Text textComponent;
    public GameObject dialoguePanel; 

  
public void DisplayInstruction() {

    if (dialogueInstruction != null && textComponent != null && dialoguePanel != null) {
       
        Debug.Log("Displaying instruction text: " + dialogueInstruction.instructionText);

        // Update the text of the UI Text component with the instruction text from the Scriptable Object
        textComponent.text = dialogueInstruction.instructionText;

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
        if (textComponent != null && textComponent.gameObject != null && dialoguePanel != null)
        {
            textComponent.gameObject.SetActive(false); 
            dialoguePanel.SetActive(false); 
            Debug.Log("Dialogue panel closed.");
        }
        else {
            Debug.LogWarning("UI Text component or panel GameObject not assigned.");
        }
    }
}
