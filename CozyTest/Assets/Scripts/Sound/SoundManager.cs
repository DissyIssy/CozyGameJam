using System;
using UnityEngine;
using Yarn.Unity;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private DialogueRunner dialogueRunner;
    public AudioSource sfxPlayer;
    public AudioSource backgroundPlayer;

    [Header("Sounds")]
    public AudioClip glitchSound;
    public AudioClip pickUpSound;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (dialogueRunner != null)
        {
            dialogueRunner.AddCommandHandler("StopMusic", StopMusic);
            dialogueRunner.AddCommandHandler<string>("PlaySFX", PlaySFX);
        }
        else
        {
            Debug.LogError($"DialogueRunner missing on {gameObject.name}! Can't register PutOnHat.");
        }
    }

    public void PlaySFX(string soundName)
    {
        AudioClip clipToPlay = null;
        
        switch (soundName.ToLower())
        {
            case "glitchsound":
                clipToPlay = glitchSound;
                break;
            case "pickupsound":
                clipToPlay = pickUpSound;
                break;

            default:
                Debug.LogWarning($"SoundManager: No clip found matching name '{soundName}'");
                return;
        }
        sfxPlayer.PlayOneShot(clipToPlay);
    }

    public void StopMusic()
    {
        if (backgroundPlayer != null)
        {
            Debug.Log("stop Music");
            backgroundPlayer.Stop();
        }
    }
}
