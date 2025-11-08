using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [Header("Timer Settings")]
    public float totalTime = 60f;
    public TMP_Text timerText;

    [Header("Lose Screen")]
    public GameObject loseScreen;

    [Header("Pause Menu Reference")]
    public PauseMenu pauseMenu;

    private float currentTime;
    private bool timerRunning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        FindUIReferences();
    }

    void Start()
    {
        if (timerText == null || loseScreen == null)
            FindUIReferences();

        if (loseScreen != null)
            loseScreen.SetActive(false);

        ShowTimer();
        StartTimer();
    }

    private void FindUIReferences()
    {
        if (timerText == null)
        {
            GameObject timerObj = GameObject.Find("TimerText_TMP");
            if (timerObj != null)
            {
                timerText = timerObj.GetComponent<TMP_Text>();
            }
            
        }

        if (loseScreen == null)
        {
            GameObject loseScreenObj = GameObject.Find("LoseScreen");
            if (loseScreenObj != null)
            {
                loseScreen = loseScreenObj;
            }
        }
    }


    void Update()
    {
        if (timerRunning)
        {
            currentTime -= Time.deltaTime;

            UpdateTimerDisplay();

            if (currentTime <= 0f)
            {
                currentTime = 0f;
                timerRunning = false;
                TimerEnded();
            }
        }
    }

    public void StartTimer()
    {
        currentTime = totalTime;
        timerRunning = true;
        UpdateTimerDisplay();

        if (loseScreen != null)
            loseScreen.SetActive(false);
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            float displayTime = Mathf.Max(0f, currentTime);

            int minutes = Mathf.FloorToInt(displayTime / 60f);
            int seconds = Mathf.FloorToInt(displayTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void TimerEnded()
    {
        timerRunning = false;
        ShowLoseScreen();
    }

    private void ShowLoseScreen()
    {
        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
            HideTimer();
            HidePauseButton();
            StopTimer();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            StopGameplayMusic();

            PlayLoseSound();
        }
        
    }

    private void PlayLoseSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayLoseSound();
        }
        else
        {
            Debug.LogWarning("AudioManager instance not found!");
        }
    }

    private void HidePauseButton()
    {
        if (pauseMenu != null)
        {
            pauseMenu.HidePauseButton();
        }
    }

    private void StopGameplayMusic()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopMusic();
        }
    }

    private void HideTimer()
    {

        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }

    }

    private void ShowTimer()
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
        }
    }



    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public float GetRemainingTime()
    {
        return currentTime;
    }

}
