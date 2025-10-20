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

    [Header("Music")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip storyMontageMusic;
    [SerializeField] private AudioClip gameplayMusic;

    private AudioSource audioSource;
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

        audioSource = GetComponent<AudioSource>();

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.volume = 0.7f;
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
        if (sparkleSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(sparkleSound);
        }
        else
        {
            Debug.LogWarning("Sparkle sound or AudioSource not set!");
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
