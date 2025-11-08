using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    [Header("UI References")]
    public GameObject pauseMenuUI;
    public GameObject pauseButton;
    public GameObject pauseControlsUI;

    [Header("Settings")]
    public string mainMenuSceneName = "MainMenu";

    private bool isPaused = false;
    private FPController playerController;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        pauseControlsUI.SetActive(false);
        pauseButton.SetActive(true);

        playerController = FindFirstObjectByType<FPController>();

    }

    void Update()
    {
        PlayerInventory playerInventory = FindFirstObjectByType<PlayerInventory>();
        bool winPanelActive = playerInventory != null && playerInventory.winPanel != null && playerInventory.winPanel.activeInHierarchy;

        if (Input.GetKeyDown(KeyCode.Escape) && !winPanelActive)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void SetGamePaused(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0f;
            isPaused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            DisablePlayerInput();
        }
        else
        {
            Time.timeScale = 1f;
            isPaused = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            EnablePlayerInput();
        }
    }
    public void PauseGame()
    {
        Debug.Log("Pause button pressed!");
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        SetGamePaused(true);

    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        pauseControlsUI.SetActive(false);
        pauseButton.SetActive(true);
        SetGamePaused(false);
    }

    public void OpenControls()
    {
        pauseMenuUI.SetActive(false);
        pauseControlsUI.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void HidePauseButton()
    {
        if (pauseButton != null)
        {
            pauseButton.SetActive(false);
        }
    }

    private void DisablePlayerInput()
    {
        if (playerController != null)
            playerController.enabled = false;
    }

    private void EnablePlayerInput()
    {
        if (playerController != null)
            playerController.enabled = true;
    }

}
