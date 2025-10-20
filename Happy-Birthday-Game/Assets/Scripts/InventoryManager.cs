using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject InventoryCanvas;
    public GameObject InventoryMenu;
    public InventorySlotUI[] inventorySlotsUI;

    [Header("Win Screen")]
    public GameObject winPanel; // Drag your win panel here in Inspector

    private bool menuActivated;
    private PlayerInventory playerInventory;

    void Start()
    {
        // Find player inventory
        playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.OnAllIngredientsCollected.AddListener(OnGameWon);
        }

        // Ensure UI starts in correct state
        if (InventoryMenu != null)
        {
            InventoryMenu.SetActive(false);
        }
        if (InventoryCanvas != null)
        {
            InventoryCanvas.SetActive(true);
        }
        if (winPanel != null)
        {
            winPanel.SetActive(false); // Start with win panel hidden
        }

        Debug.Log("Inventory ready - Press I to open");
    }

    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame && winPanel != null && !winPanel.activeSelf)
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
            Debug.Log("Inventory opened");
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("Inventory closed");
        }
    }

    public void UpdateInventoryDisplay(PlayerInventory inventory)
    {
        Debug.Log("Updating inventory with " + inventory.collectedIngredients.Count + " items");

        // Clear all slots
        for (int i = 0; i < inventorySlotsUI.Length; i++)
        {
            inventorySlotsUI[i].ClearSlot();
        }

        // Fill slots with collected items
        foreach (Collectible ingredient in inventory.collectedIngredients)
        {
            int slotIndex = -1;
            switch (ingredient.ingredientName)
            {
                case "Icing": slotIndex = 0; break;
                case "Flour": slotIndex = 1; break;
                case "Sprinkles": slotIndex = 2; break;
                case "Tears": slotIndex = 3; break;
            }

            if (slotIndex != -1)
            {
                inventorySlotsUI[slotIndex].SetSlot(ingredient.ingredientSprite, ingredient.ingredientName);
            }
        }

        // Show briefly when new item collected
        if (!menuActivated)
        {
            ShowInventoryBriefly();
        }

        // CRITICAL: Check if this update completed the collection
        // This ensures the inventory shows the 4th item BEFORE the win panel appears
        if (inventory.collectedIngredients.Count >= 4)
        {
            Debug.Log("Inventory updated with all 4 items - preparing win screen");
            // Small delay to ensure player sees the filled inventory
            StartCoroutine(ShowWinPanelAfterInventory());
        }
    }

    private void ShowInventoryBriefly()
    {
        StartCoroutine(ShowTemporaryInventory());
    }

    private IEnumerator ShowTemporaryInventory()
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
        Debug.Log("Win condition detected - waiting for inventory update first");
        // The actual win panel show is now handled in UpdateInventoryDisplay
        // after the inventory is visually updated
    }

    // This coroutine ensures the inventory shows the completed state BEFORE the win panel
    private IEnumerator ShowWinPanelAfterInventory()
    {
        Debug.Log("Showing completed inventory first...");

        // Force show the inventory so player can see all slots filled
        if (!menuActivated)
        {
            InventoryMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Wait a moment so player can see the completed inventory
        yield return new WaitForSeconds(2f);

        // Now show the win panel
        ShowWinPanel();
    }

    public void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);

            // Hide the inventory menu when win panel shows
            if (InventoryMenu != null)
            {
                InventoryMenu.SetActive(false);
            }

            // Show cursor and unlock it
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Disable player movement
            FPController playerController = FindObjectOfType<FPController>();
            if (playerController != null)
            {
                playerController.enabled = false;
            }

            Debug.Log(" WIN PANEL ACTIVATED! Inventory was shown first.");
        }
        else
        {
            Debug.LogError("Win panel reference is missing!");
        }
    }

    // Optional: Add a restart method
    public void RestartGame()
    {
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}