using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Reference: Unity3D (2023) How to Collect Items (Unity Tutorial). 15 March. Available at: https://youtu.be/EfUCEwKmcjc (Accessed: 14 August 2025).
//Reference: OpenAI, 2025. ChatGPT (GPT-5 mini) [AI language model]. Personal assistance with Debugging script. 19 August 2025.

public class PlayerInventory : MonoBehaviour
{
    public List<Collectible> collectedIngredients = new List<Collectible>();
    public UnityEvent OnAllIngredientsCollected;
    public GameObject winPanel;
    public float winPanelDelay;

    void Start()
    {
        // Hide win panel at start
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }

    public void CollectIngredient(Collectible collectible)
    {
        if (!collectedIngredients.Contains(collectible))
        {
            collectedIngredients.Add(collectible);

            // ADDED DEBUG LINES
            Debug.Log("=== COLLECTION LOG ===");
            Debug.Log("Just collected: " + collectible.ingredientName);
            Debug.Log("Total collected so far: " + collectedIngredients.Count);

            // Debug: List all collected items
            foreach (Collectible item in collectedIngredients)
            {
                Debug.Log(" - " + item.ingredientName);
            }
            // END OF ADDED DEBUG LINES

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
                Debug.Log("ALL INGREDIENTS COLLECTED! YOU WIN!");

                // Use this instead:
                StartCoroutine(DelayedWinPanel());
            }
        }
    }

    private IEnumerator DelayedWinPanel()
    {
        yield return new WaitForSeconds(winPanelDelay);
        ShowWinPanel();
    }

    private void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);

            // Optional: Unlock cursor for win panel
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("Win panel shown!");
        }
        else
        {
            Debug.LogError("Win Panel reference not set in PlayerInventory!");
        }
    }

    public bool HasAllIngredients()
    {
        return collectedIngredients.Count >= 4;
    }
}