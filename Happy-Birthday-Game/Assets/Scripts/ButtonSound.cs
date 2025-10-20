using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonSound : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(PlayButtonSound);
        }
        else
        {
            Debug.LogWarning("ButtonSound script attached to object without Button component: " + gameObject.name);
        }
    }

    private void PlayButtonSound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySparkleSound();
        }
        else
        {
            Debug.LogWarning("SoundManager instance not found!");
        }
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(PlayButtonSound);
        }
    }
}
