using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Reference: Unity3D (2023) How to Collect Items (Unity Tutorial). 15 March. Available at: https://youtu.be/EfUCEwKmcjc (Accessed: 14 August 2025).
//Reference: OpenAI, 2025. ChatGPT (GPT-5 mini) [AI language model]. Personal assistance with Debugging script. 19 August 2025.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public List<Collectible> collectedIngredients = new List<Collectible>();
    public UnityEvent OnAllIngredientsCollected; 

    public void CollectIngredient(Collectible collectible)
    {
        if (!collectedIngredients.Contains(collectible))
        {
            collectedIngredients.Add(collectible);
            Debug.Log("Collected: " + collectible.ingredientName);

            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager != null)
            {
                inventoryManager.UpdateInventoryDisplay(this);
            }
            else
            {
                Debug.LogError("No InventoryManager found in scene!");
            }

            // Win condition
            if (collectedIngredients.Count >= 4)
            {
                OnAllIngredientsCollected.Invoke();
            }
      

            if (collectedIngredients.Count >= 4)
            {
                OnAllIngredientsCollected.Invoke();
                Debug.Log("ALL INGREDIENTS COLLECTED! YOU WIN!");
            }
        }
    }

    public bool HasAllIngredients()
    {
        return collectedIngredients.Count >= 4;
    }
}