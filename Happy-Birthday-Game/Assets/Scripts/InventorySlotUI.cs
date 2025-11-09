using UnityEngine;
using UnityEngine.UI;
/*
 * Reference: OpenAI. (2024). ChatGPT. https://chat.openai.com
 * AI-assisted development for [specific functionality].
 * Code adapted, modified, and implemented by developer.
 */


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
