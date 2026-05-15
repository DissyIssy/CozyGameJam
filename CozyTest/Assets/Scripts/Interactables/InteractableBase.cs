using UnityEngine;
using Yarn.Unity;

public abstract class InteractableBase : MonoBehaviour
{
    //Make it so the dialogue runner is found automatically
    [SerializeField] protected DialogueRunner dialogueRunner;
    public virtual bool IsPickUpAble => false;
    
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
