using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Reference: Unity3D (2023) How to Collect Items (Unity Tutorial). 15 March. Available at: https://youtu.be/EfUCEwKmcjc (Accessed: 14 August 2025).
//Reference: OpenAI, 2025. ChatGPT (GPT-5 mini) [AI language model]. Personal assistance with Debugging script. 19 August 2025.

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfCakeIngrediants { get; private set; }
    
    public UnityEvent<PlayerInventory> OnCakeIngrediantCollected;

    public void CakeIngrediantCollected()
    {
        NumberOfCakeIngrediants++;
        Debug.Log("Collected! Inventory count: " + NumberOfCakeIngrediants);
        OnCakeIngrediantCollected.Invoke(this);
    }



}
