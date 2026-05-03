using UnityEngine;
using Yarn.Unity;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private SO_InteractableInfo interactableInfo;
    [SerializeField] private DialogueRunner dialogueRunner;

    public void InitDialogue()
    {
        if (interactableInfo.isPickUpAble && TaskManager.Instance.taskStarted)
        {
            dialogueRunner.StartDialogue(interactableInfo.yarnID_PickUpDialogue);
        }
        else
        {
            dialogueRunner.StartDialogue(interactableInfo.yarnID_Interactable);
        }
    }
    
    public void StopDialouge()
    {
        if (dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.Stop();
        }
    }

    public void PickUp()
    {
        if (!TaskManager.Instance.taskStarted)
        {
            return;
        }
        
        if (interactableInfo.isPickUpAble)
        {
            TaskManager.Instance.RegisterClean();
            Debug.Log("Picked up");
            Destroy(transform.parent.gameObject);
            //transform.root.gameObject.SetActive(false);
        }
    }
}
