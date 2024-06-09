using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour {
    public DialogueManager dialogueManager; 

    // Assign the reference to the DialogueManager script in the Unity Editor
    public void SetDialogueManager(DialogueManager manager) {
        dialogueManager = manager;
    }

   
    public void OnButtonClick() {
        // Check if the DialogueManager reference is valid
        if (dialogueManager != null) {
            // Call the CloseInstruction method of the DialogueManager script
            dialogueManager.CloseInstruction();
        }
        else {
            Debug.LogWarning("DialogueManager reference not assigned.");
        }
    }
}
