using UnityEngine;
using Yarn.Unity;

public abstract class InteractableBase : MonoBehaviour
{
    [SerializeField] protected DialogueRunner dialogueRunner;
    public virtual bool IsPickUpAble => false;

    //Makes it so the dialogue runner is found automatically
    protected virtual void Awake()
    {
        if (dialogueRunner == null)
        {
            dialogueRunner = FindAnyObjectByType<DialogueRunner>();
            if (dialogueRunner == null)
            {
                Debug.LogWarning($"DialogueRunner not found in the scene for {gameObject.name}!");
            }
        }
    }
    
    //Needs to be on the same gameobject as the trigger
    public abstract void InitDialogue();

    public virtual void StopDialogue()
    {
        if (dialogueRunner != null && dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.Stop();
        }
    }

    public virtual void OnInteract()
    {
        
    }
    
}
