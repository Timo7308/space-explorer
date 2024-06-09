using UnityEngine;

//Handling of life and collected items. 
[System.Serializable]
public class PlayerStats {

    public int currentLifePoints; 
    public int maxLifePoints;
    public int itemCount;
    public int maxItemCount; 

    public PlayerStats(int maxLifePoints, int maxItemCount) {

        this.maxLifePoints = maxLifePoints;
        this.currentLifePoints = maxLifePoints;
        this.maxItemCount = maxItemCount;
        this.itemCount = 0;
    }

    public void TakeDamage(int damageAmount) {

        currentLifePoints -= damageAmount;
        if (currentLifePoints < 0)
            currentLifePoints = 0;
    }

    public void CollectItem() {
        itemCount++;
    }

    public bool IsDead() {
        return currentLifePoints <= 0;
    }
}
