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

    private float currentTime;
    private bool timerRunning = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartTimer();
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

            Time.timeScale = 0f;
        }
        else
        {
            Debug.LogWarning("Lose screen reference not set in TimerManager!");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
