using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TMP_Text counterText; //Text object
    private int itemCount = 0; //Current number of collected items. 
    private int maxItemCount = 4; //Max number of collectible items.


    //If player is close enough to an item, increase counter by one. 
    public void CollectItem()
    {
        itemCount++;
        UpdateCounterText();
    }


    //Display number of collected minerals
    private void UpdateCounterText()
    {
        if (counterText != null)
        {
            counterText.text = "Minerals: " + itemCount.ToString() + " / " + maxItemCount.ToString();
        }
    }
}
