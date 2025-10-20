using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections; 

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

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySparkleSound();
        }

        buttonLocked = true;

        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];

            StartCoroutine(UnlockButtonAfterDelay(0.1f));
        }
        else
        {
            if (controlsMenu != null)
                controlsMenu.SetActive(true);

            buttonLocked = false;
            gameObject.SetActive(false);
        }

    }

    private IEnumerator UnlockButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        buttonLocked = false;
    }
}
