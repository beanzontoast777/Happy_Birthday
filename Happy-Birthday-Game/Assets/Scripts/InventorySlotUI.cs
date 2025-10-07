using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image itemIcon;

    void Start()
    {
        ClearSlot();
    }

    public void SetSlot(Sprite sprite, string itemName)
    {
        if (itemIcon != null)
        {
            itemIcon.sprite = sprite;
            itemIcon.enabled = true;
        }
    }

    public void ClearSlot()
    {
        if (itemIcon != null)
        {
            itemIcon.sprite = null;
            itemIcon.enabled = false;
        }
    }
}
