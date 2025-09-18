using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//Reference: Unity3D (2023) How to Collect Items (Unity Tutorial). 15 March. Available at: https://youtu.be/EfUCEwKmcjc (Accessed: 14 August 2025).
public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI cakeIngrediantText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cakeIngrediantText = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateCakeIngrediantText(PlayerInventory playerInventory)
    {
        cakeIngrediantText.text = playerInventory.NumberOfCakeIngrediants.ToString();
    }

}
