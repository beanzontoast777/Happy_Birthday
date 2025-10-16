using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsMenu : MonoBehaviour
{
    public GameObject page1; 
    public GameObject page2;

    void Start()
    {
        page1.SetActive(true);
        page2.SetActive(false);
    }

    public void ShowNextPage()
    {
        page1.SetActive(false);
        page2.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

}
