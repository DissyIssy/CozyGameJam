using UnityEngine;
using Yarn.Unity;

public abstract class ItemInteraction : InteractableBase
{
    public SO_InteractableInfo interactableInfo;
    public override bool IsPickUpAble => interactableInfo != null && interactableInfo.isPickUpAble;
    public override void InitDialogue()
    {
        dialogueRunner.StartDialogue(interactableInfo.yarnID_Interactable);
    }

    public override abstract void OnInteract();
}
