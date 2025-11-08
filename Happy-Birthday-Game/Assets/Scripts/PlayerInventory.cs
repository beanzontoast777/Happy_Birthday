using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

           
            Debug.Log("COLLECTION LOG");
            Debug.Log("Just collected: " + collectible.ingredientName);
            Debug.Log("Total collected so far: " + collectedIngredients.Count);

            
            foreach (Collectible item in collectedIngredients)
            {
                Debug.Log(" - " + item.ingredientName);
            }
            

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

            
            if (collectedIngredients.Count >= 4)
            {
                OnAllIngredientsCollected.Invoke();
                Debug.Log("ALL INGREDIENTS COLLECTED! YOU WIN!");

               
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

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (TimerManager.Instance != null)
            {
                TimerManager.Instance.HideTimerAndPauseText();
            }

            if (TimerManager.Instance != null)
            {
                TimerManager.Instance.StopTimer();
            }

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayWinSound();
                AudioManager.Instance.StopMusic();
            }

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

    public void GoToMainMenu()
    {
        Debug.Log("Returning to main menu...");

        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");

    }

    public void TestButton()
    {
        Debug.Log("TEST: Button is clicked! This works!");
    }



}