using UnityEngine;

public class PauseControls : MonoBehaviour
{
    [Header("Pages")]
    public GameObject page1;
    public GameObject page2;

    [Header("Pause Menu Reference")]
    public GameObject pauseMenuUI;

    void Start()
    {
        page1.SetActive(true);
        page2.SetActive(false);
    }

    public void GoToPage2()
    {
        page1.SetActive(false);
        page2.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        page2.SetActive(false);
        page1.SetActive(true);
        pauseMenuUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
