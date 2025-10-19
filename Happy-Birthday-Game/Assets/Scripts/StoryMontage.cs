using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class StoryMontage : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text dialogueText;
    public Button continueButton;
    public GameObject controlsMenu; 

    [Header("Story Text")]
    [TextArea(3, 10)]
    public string[] dialogueLines;

    private int currentLine = 0;
    private bool buttonLocked = false;


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

        if (controlsMenu != null)
            controlsMenu.SetActive(false);
    }

     public void OnContinuePressed()
    {
        if (buttonLocked) return;
        buttonLocked = true;

        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
            StartCoroutine(UnlockButtonNextFrame());
        }
        else
        {
            if (controlsMenu != null)
                controlsMenu.SetActive(true);

            buttonLocked = false;
            gameObject.SetActive(false);
        }

    }

    private System.Collections.IEnumerator UnlockButtonNextFrame()
    {
        yield return null;
        buttonLocked = false;
    }
}
