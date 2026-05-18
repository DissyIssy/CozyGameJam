using UnityEngine;
using Yarn.Unity;

public class CarenWearsHat : MonoBehaviour
{
    // Make sure to drag your DialogueRunner here via the Inspector
    [SerializeField] private DialogueRunner dialogueRunner; 
    public GameObject hat;

    private void Awake()
    {
        if (dialogueRunner != null)
        {
            // Registers a truly global command. No GameObject targets required!
            dialogueRunner.AddCommandHandler("PutOnHat", PutOnHatCommand);
        }
        else
        {
            Debug.LogError($"DialogueRunner missing on {gameObject.name}! Can't register PutOnHat.");
        }
    }

    // This is the actual logic that runs
    private void PutOnHatCommand()
    {
        if (hat != null)
        {
            hat.SetActive(true);
            Debug.Log("Caren's hat has been set to active!");
        }
        else
        {
            Debug.LogWarning("PutOnHat failed: The hat GameObject reference is empty.");
        }
    }
}