using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip sparkleSound;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySparkleSound()
    {
        if (sparkleSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(sparkleSound);
        }
        else
        {
            Debug.LogWarning("Sparkle sound or AudioSource not set!");
        }
    }



}
