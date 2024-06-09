using System;

public class PlayerStats {

    //Events for life point changes, item collection 
    //and Game over and Game won 
    public event Action<int> OnLifeChanged;
    public event Action<int> OnItemCollected;
    public event Action OnGameOver;
    public event Action OnGameWon;

    public int currentLifePoints;
    public int maxLifePoints;
    public int itemCount;
    public int maxItemCount;

    //Life points and max number of items 
    public PlayerStats(int lifePoints, int maxItems) {
        currentLifePoints = lifePoints;
        maxLifePoints = lifePoints;
        itemCount = 0;
        maxItemCount = maxItems;
    }

    //Handle damage of player
    public void TakeDamage(int damageAmount) {
        currentLifePoints -= damageAmount;
        OnLifeChanged?.Invoke(currentLifePoints);

        if (currentLifePoints <= 0) {
            OnGameOver?.Invoke();
        }
    }

    // Handle item collection
    public void CollectItem() {
        itemCount++;
        OnItemCollected?.Invoke(itemCount);

        if (itemCount >= maxItemCount) {
            OnGameWon?.Invoke();
        }
    }

    //If lifepoints are zero the player is dead
    public bool IsDead() {
        return currentLifePoints <= 0;
    }
}
