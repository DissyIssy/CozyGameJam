using System;
using UnityEngine;
using Yarn.Unity;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public AudioSource sfxPlayer;
    
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
    }

    //Can be called from dialogue, needs to be converted to string since yarnspinner only understands basic types
    [YarnCommand("PlaySFX")]
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
}
