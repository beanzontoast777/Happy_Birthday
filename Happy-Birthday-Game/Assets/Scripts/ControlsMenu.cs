using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsMenu : MonoBehaviour
{
    public GameObject page1; 
    public GameObject page2;

    void Start()
    {
        Debug.Log("ControlsMenu Started");
        page1.SetActive(true);
        page2.SetActive(false);
    }

    public void ShowNextPage()
    {
        Debug.Log("ShowNextPage called");
        page1.SetActive(false);
        page2.SetActive(true);
    }

    public void StartGame()
    {
        Debug.Log("StartGame called - Loading GameScene");
        SceneManager.LoadScene("GameScene");
    }

}
