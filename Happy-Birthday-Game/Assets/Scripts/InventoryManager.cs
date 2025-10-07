using UnityEngine;
using UnityEngine.InputSystem;
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

    // Call this from PlayerInventory when an item is collected
    public void UpdateInventoryDisplay(PlayerInventory inventory)
    {
        // Clear all slots first
        foreach (InventorySlotUI slot in inventorySlotsUI)
        {
            slot.ClearSlot();
        }

        // Fill slots with whatever ingredients we've collected
        for (int i = 0; i < inventory.collectedIngredients.Count && i < inventorySlotsUI.Length; i++)
        {
            Collectible ingredient = inventory.collectedIngredients[i];
            inventorySlotsUI[i].SetSlot(ingredient.ingredientSprite, ingredient.ingredientName);
        }

        // Show inventory briefly when new item is collected
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