using UnityEngine;
using Yarn.Unity;

public abstract class InteractableBase : MonoBehaviour
{
    [SerializeField] protected DialogueRunner dialogueRunner;

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
