using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
        Debug.Log("Play button pressed! Loading game scene...");
    }

    public void QuitGame()
    {
        Debug.Log("Quit pressed!");
        Application.Quit();
    }




}
