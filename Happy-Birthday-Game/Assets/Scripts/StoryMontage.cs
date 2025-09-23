using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class StoryMontage : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text dialogueText;
    public Button continueButton;

    [Header("Story Text")]
    [TextArea(3, 10)]
    public string[] dialogueLines;

    private int currentLine = 0;
   

    void Start()
    {
        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            Debug.LogWarning("No dialogue lines set!");
            return;
        }

        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(OnContinuePressed);

        dialogueText.text = dialogueLines[currentLine];
    }

    void OnContinuePressed()
    {
        if (currentLine >= dialogueLines.Length - 1)
        {
            SceneManager.LoadScene("GameScene");
            return;
        }

        currentLine++;
        dialogueText.text = dialogueLines[currentLine];
    }
}
