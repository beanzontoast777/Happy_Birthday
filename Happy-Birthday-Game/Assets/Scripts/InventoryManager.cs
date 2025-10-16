using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic; 
using System.Collections;
public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryCanvas;
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
            //InventoryMenu.SetActive(false);
            StartCoroutine(inventoryOn());
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
        Debug.Log("=== INVENTORY UPDATE ===");
        Debug.Log("Collected items: " + inventory.collectedIngredients.Count);

        // Debug: Show ALL collected items
        foreach (Collectible item in inventory.collectedIngredients)
        {
            Debug.Log("Collected: " + item.ingredientName + " (Sprite: " + (item.ingredientSprite != null) + ")");
        }

        // Clear all slots first
        for (int i = 0; i < inventorySlotsUI.Length; i++)
        {
            inventorySlotsUI[i].ClearSlot();
        }

        // Fill ALL slots with ALL collected items - FIXED
        foreach (Collectible ingredient in inventory.collectedIngredients)
        {
            int slotIndex = -1;

            // Match your ingredient names
            switch (ingredient.ingredientName)
            {
                case "Icing": slotIndex = 0; break;
                case "Flour": slotIndex = 1; break;
                case "Sprinkles": slotIndex = 2; break;
                case "Tears": slotIndex = 3; break;
                default:
                    Debug.LogError("Unknown ingredient: " + ingredient.ingredientName);
                    break;
            }

            if (slotIndex != -1 && slotIndex < inventorySlotsUI.Length)
            {
                Debug.Log("Assigning " + ingredient.ingredientName + " to slot " + slotIndex);
                inventorySlotsUI[slotIndex].SetSlot(ingredient.ingredientSprite, ingredient.ingredientName);
            }
            else
            {
                Debug.LogError("Invalid slot index for: " + ingredient.ingredientName);
            }
        }

        // Debug: Check what's actually in slots after assignment
        Debug.Log("=== SLOT STATUS AFTER UPDATE ===");
        for (int i = 0; i < inventorySlotsUI.Length; i++)
        {
            bool hasSprite = inventorySlotsUI[i].itemIcon != null &&
                            inventorySlotsUI[i].itemIcon.sprite != null &&
                            inventorySlotsUI[i].itemIcon.enabled;
            Debug.Log("Slot " + i + ": " + (hasSprite ? "FILLED" : "EMPTY"));
        }

        // Show inventory briefly
        if (!menuActivated)
        {
            ShowInventoryBriefly();
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

    IEnumerator inventoryOn()
    {
        InventoryCanvas.SetActive(false);
        InventoryMenu.SetActive(true);

        yield return new WaitForSeconds(.01f);

        InventoryMenu.SetActive(false);
        InventoryCanvas.SetActive(true);
    }
}