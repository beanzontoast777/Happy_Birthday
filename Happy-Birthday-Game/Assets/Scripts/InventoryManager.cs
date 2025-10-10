using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic; 
using System.Collections;
public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public InventorySlotUI[] inventorySlotsUI; // Assign 4 slots in Inspector

    private bool menuActivated;

    void Start()
    {
        // Find player inventory and setup
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            // Optional: You can connect to the win event if you want UI feedback
            playerInventory.OnAllIngredientsCollected.AddListener(OnGameWon);
        }

        // Ensure inventory is hidden at start
        if (InventoryMenu != null)
            InventoryMenu.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        menuActivated = !menuActivated;
        InventoryMenu.SetActive(menuActivated);

        if (menuActivated)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void UpdateInventoryDisplay(PlayerInventory inventory)
    {
        // DELETE THE OLD VERSION and replace with this:

        Debug.Log("Updating inventory display with " + inventory.collectedIngredients.Count + " items");

        // Clear all slots first
        for (int i = 0; i < inventorySlotsUI.Length; i++)
        {
            inventorySlotsUI[i].ClearSlot();
        }

        // Create a mapping of ingredient names to slot indices
        Dictionary<string, int> slotMapping = new Dictionary<string, int>()
    {
        {"Icing", 0},
        {"Flour", 1},
        {"Sprinkles", 2},
        {"Tears", 3}
    };

        // Fill the correct slots
        foreach (Collectible ingredient in inventory.collectedIngredients)
        {
            if (slotMapping.ContainsKey(ingredient.ingredientName))
            {
                int slotIndex = slotMapping[ingredient.ingredientName];
                if (slotIndex < inventorySlotsUI.Length)
                {
                    Debug.Log($"Assigning {ingredient.ingredientName} to slot {slotIndex}");
                    inventorySlotsUI[slotIndex].SetSlot(ingredient.ingredientSprite, ingredient.ingredientName);
                }
            }
            else
            {
                Debug.LogWarning($"No slot mapping found for: {ingredient.ingredientName}");
            }
        }

        // Show inventory briefly
        if (!menuActivated)
        {
            ShowInventoryBriefly();
        }
    }

    private void ShowInventoryBriefly()
    {
        StartCoroutine(ShowTemporaryInventory());
    }

    private System.Collections.IEnumerator ShowTemporaryInventory()
    {
        InventoryMenu.SetActive(true);
        yield return new WaitForSeconds(3f);

        if (!menuActivated)
        {
            InventoryMenu.SetActive(false);
        }
    }

    private void OnGameWon()
    {
        Debug.Log("UI System: Game won detected!");
        // You can add UI effects here like:
        // - Show victory screen
        // - Make inventory slots glow
        // - Play celebration animation
    }
}