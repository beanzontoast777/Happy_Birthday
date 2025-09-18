using UnityEngine;
using TMPro; 

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        dialoguePanel.SetActive(false);
    }

    public void ShowDialogue(string line)
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = line;
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    public bool IsOpen => dialoguePanel.activeSelf;
}
