using UnityEngine;
using UnityEngine.InputSystem;
public class InventoryManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject InventoryMenu;
    private bool menuActivated;

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
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
    }
}