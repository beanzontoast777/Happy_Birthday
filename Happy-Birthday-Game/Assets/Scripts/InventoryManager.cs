using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryCanvas;
    public GameObject InventoryMenu;
    public InventorySlotUI[] inventorySlotsUI;

    private bool menuActivated;

    void Start()
    {
        // Find player inventory
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.OnAllIngredientsCollected.AddListener(OnGameWon);
        }

        // Simple setup - no coroutines
        if (InventoryMenu != null)
        {
            InventoryMenu.SetActive(false);
        }
        if (InventoryCanvas != null)
        {
            InventoryCanvas.SetActive(true);
        }

        Debug.Log("Inventory ready - Press I to open");
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
        Debug.Log("UI System: Game won detected!");
    }
}