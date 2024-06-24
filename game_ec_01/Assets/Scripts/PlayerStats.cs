using System;
using UnityEngine;

public class PlayerStats {


    //Events
    public event Action<int> OnLifeChanged;
    public event Action<int> OnItemCollected;
    public event Action OnGameOver;
    public event Action OnGameWon;

    public int currentLifePoints;
    public int maxLifePoints;
    public int itemCount;
    public int maxItemCount;
    public bool hasTouchedGoal;


    //Manage lifepoints and max number of items
    public PlayerStats(int lifePoints, int maxItems) {
        currentLifePoints = lifePoints;
        maxLifePoints = lifePoints;
        itemCount = 0;
        maxItemCount = maxItems;
        hasTouchedGoal = false;
    }

    //Manage damage of player and initialize game lost if all life points are gone. 
    public void TakeDamage(int damageAmount) {
        currentLifePoints -= damageAmount;
        OnLifeChanged?.Invoke(currentLifePoints);

        if (currentLifePoints <= 0) {
            OnGameOver?.Invoke();
        }
    }

    //Check collection of items and initialize game won if all conditions are met.
    public void CollectItem() {
        itemCount++;
        OnItemCollected?.Invoke(itemCount);

        if (itemCount >= maxItemCount && hasTouchedGoal) {
            OnGameWon?.Invoke();
            GameManager.Instance.SetGameState(GameState.GameWon);
        }
    }

    //Check if goal was touched. 
    public void TouchGoal() {
        hasTouchedGoal = true;
      //  Debug.Log("Goal touched. Items collected: " + itemCount + "/" + maxItemCount);

        if (itemCount >= maxItemCount) {
            OnGameWon?.Invoke();
        }
    }

    public bool IsDead() {
        return currentLifePoints <= 0;
    }
}
