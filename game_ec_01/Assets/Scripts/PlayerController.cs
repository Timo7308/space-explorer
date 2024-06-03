using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TMP_Text counterText; // Change the type to TMPro.TextMeshProUGUI
    private int itemCount = 0;
    private int maxItemCount = 4; // Change this value to your desired maximum count

    void Start()
    {
        // Find the TextMeshPro object by tag
        counterText = GameObject.FindGameObjectWithTag("CounterText").GetComponent<TMP_Text>(); // Change GetComponent<TextMeshProUGUI>() to GetComponent<TMP_Text>()

        if (counterText == null)
        {
            Debug.LogError("CounterText is not assigned and not found by tag.");
        }
        else
        {
            UpdateCounterText();
        }
    }

    public void CollectItem()
    {
        itemCount++;
        UpdateCounterText();
    }

    private void UpdateCounterText()
    {
        if (counterText != null)
        {
            counterText.text = "Minerals: " + itemCount.ToString() + " / " + maxItemCount.ToString();
        }
    }
}
