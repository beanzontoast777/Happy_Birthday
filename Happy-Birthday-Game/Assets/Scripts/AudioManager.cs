using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Audio Credits
 * -------------
 * All music composed by Bontle Tlhapane using GarageBand
 * © 2025 All rights reserved
 */


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sound Effects")]
    [SerializeField] private AudioClip sparkleSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip collectionSound;

    [Header("Music")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip storyMontageMusic;
    [SerializeField] private AudioClip gameplayMusic;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float sparkleVolume = 0.2f;
    [Range(0f, 1f)] public float loseVolume = 0.8f;
    [Range(0f, 1f)] public float winVolume = 1.0f;
    [Range(0f, 1f)] public float musicVolume = 0.27f;
    [Range(0f, 1f)] public float collectionVolume = 0.6f;

    private AudioSource sfxSource;
    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        sfxSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.priority = 64;

        sfxSource.loop = false;
        sfxSource.volume = 1f;
        sfxSource.priority = 128;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }

        if (sceneName == "MainMenu")
        {
            PlayMainMenuMusic();
        }
        else if (sceneName == "StoryMontage")
        {
            PlayStoryMontageMusic();
        }
        else if (sceneName == "GameScene")
        {
            PlayGameplayMusic();
        }
    }

    public void PlayMainMenuMusic()
    {
        if (mainMenuMusic != null && musicSource != null)
        {
            musicSource.loop = false;
            musicSource.clip = mainMenuMusic;
            musicSource.Play();
            Debug.Log("Main menu music started: " + mainMenuMusic.name);
        }
    }

    public void PlayStoryMontageMusic()
    {
        if (storyMontageMusic != null && musicSource != null)
        {
            musicSource.loop = true;
            musicSource.clip = storyMontageMusic;
            musicSource.Play();
            Debug.Log("Story montage music started (looping): " + storyMontageMusic.name);
        }
    }

    public void PlayGameplayMusic()
    {
        if (gameplayMusic != null && musicSource != null)
        {
            musicSource.loop = true;
            musicSource.clip = gameplayMusic;
            musicSource.Play();

            musicSource.ignoreListenerPause = true;

            Debug.Log("Gameplay music started (looping, unpausable): " + gameplayMusic.name);
        }
    }

    public void PlaySparkleSound()
    {
        if (sparkleSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(sparkleSound, sparkleVolume);
        }
        else
        {
            Debug.LogWarning("Sparkle sound or AudioSource not set!");
        }
    }

    public void PlayLoseSound()
    {
        if (loseSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(loseSound, loseVolume);
            Debug.Log("Lose sound played: " + loseSound.name);
        }
        else
        {
            Debug.LogWarning("Lose sound or AudioSource not set!");
        }
    }

    public void PlayWinSound()
    {
        if (winSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(winSound, winVolume);
            Debug.Log("Win sound played: " + winSound.name);
        }
        else
        {
            Debug.LogWarning("Win sound or AudioSource not set!");
        }
    }

    public void PlayCollectionSound()
    {
        if (collectionSound != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(collectionSound, collectionVolume);
            Debug.Log("Collection sound played: " + collectionSound.name);
        }
        else
        {
            Debug.LogWarning("Collection sound or AudioSource not set!");
        }
    }

    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
            Debug.Log("Music stopped");
        }
    }
    

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
