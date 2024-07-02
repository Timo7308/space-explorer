using UnityEngine;
using TMPro;

public class StoryManager : MonoBehaviour {
    public StoryInstruction storyInstruction; 
    public TMP_Text storyText;
    public GameObject storyPanel; 
    public TMP_Text resultText; // Text component for the result

    public ResultInstruction resultInstruction; // Reference to the ResultInstruction ScriptableObject

    // Flag to track if the story panel has been displayed
    private bool storyPanelDisplayed = false;

    void Start() {
        // Example: Loading resultInstruction if not assigned in the Inspector
        if (resultText == null) {
            resultText = GetComponentInChildren<TMP_Text>();
        }
    }

    // Method to display the story instruction panel
    public void DisplayInstruction() {
        if (!storyPanelDisplayed && storyInstruction != null && storyText != null && storyPanel != null) {
            // Update the text of the UI Text component with the instruction text from the Scriptable Object
            storyText.text = storyInstruction.instructionText;

            // Activate the story panel to show the window
            storyPanel.SetActive(true);

            // Set the flag to true after displaying the panel
            storyPanelDisplayed = true;
        } else {
            Debug.LogWarning("Story instruction, UI Text component, or panel GameObject not assigned, or story panel already displayed.");
        }
    }

    // Method to close the story instruction panel
    public void CloseInstruction() {
        if (storyText != null && storyText.gameObject != null && storyPanel != null) {
            storyText.gameObject.SetActive(false); 
            storyPanel.SetActive(false); 
            Debug.Log("Story panel closed.");
        } else {
            Debug.LogWarning("UI Text component or story panel GameObject not assigned.");
        }
    }

    // Method to display the result instruction panel
    public void DisplayResultInstruction() {
        Debug.Log("StoryManager: DisplayResultInstruction called");
        if (resultText.gameObject!= null && resultText != null) {
            // Update the text of the UI Text component with the instruction text from the Scriptable Object
            resultText.text = resultInstruction.resultText;
            resultText.gameObject.SetActive(true); // Activate resultText
            Debug.Log("Result instruction displayed: " + resultInstruction.resultText);
        } else {
            Debug.LogWarning("Result Text component or result instruction not assigned.");
        }
    }

    public void CloseResultInstruction() {
        Debug.Log("StoryManager: CloseResultInstruction called");
        if (resultText != null && resultText.gameObject != null) {
            resultText.gameObject.SetActive(false);
        } else {
            Debug.LogWarning("Result Text component not assigned.");
        }
    }
}

