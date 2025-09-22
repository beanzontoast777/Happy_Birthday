using UnityEngine;
using UnityEngine.InputSystem;
public class InventoryManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject InventoryMenu;
    private bool menuActivated;

    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
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