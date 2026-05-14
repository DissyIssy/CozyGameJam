using UnityEngine;
using Yarn.Unity;

public abstract class ItemInteraction : InteractableBase
{
    [SerializeField] protected SO_InteractableInfo interactableInfo;
    public override bool IsPickUpAble => interactableInfo != null && interactableInfo.isPickUpAble;
    public override void InitDialogue()
    {
        Debug.Log("Pressed");
        dialogueRunner.StartDialogue(interactableInfo.yarnID_Interactable);
    }

    public override abstract void OnInteract();
    
    //Currently WIP to be replaced in lightItemInteractable
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
