using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.Rendering.BoolParameter;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image speechBubbleImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            return;
        }

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    public void ShowDialogue(string line, float displayTime = 3f)
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        if (dialogueText != null)
            dialogueText.text = line;
        else
            Debug.LogError("Dialogue Text is not assigned!");

        Invoke("HideDialogue", displayTime);

    }

    public void HideDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (dialogueText != null)
            dialogueText.text = "";
    }

    public bool IsOpen => dialoguePanel != null && dialoguePanel.activeSelf;
}
