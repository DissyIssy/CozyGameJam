using System;
using System.ComponentModel.Design.Serialization;
using GameEvents.Manager;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class PlayerInteraction : MonoBehaviour
{
    private InteractableBase currentTarget;
    private PlayerInputActions playerInputActions;

    //You can surely combine the character and item interaction into a single function
    //Also be aware in the case that both cases are true.

    private void OnTriggerEnter(Collider other)
    {
        Transform targetTransform = null;
        
        if (other.TryGetComponent(out InteractableBase interactable))
        {
            currentTarget = interactable;
            
            //Send out event for UI
            EnteredInteractableTrigger_Event e = new EnteredInteractableTrigger_Event();
            e.interactableTransform = interactable.transform;
            e.isPickUpAble = currentTarget.IsPickUpAble;
            GameEventManager.Raise(e);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InteractableBase interactable))
        {
            if (currentTarget != null && currentTarget == interactable)
            {
                currentTarget.StopDialogue();
                currentTarget = null;
            
                // Send out event for UI
                ExitedInteractableTrigger_Event e = new ExitedInteractableTrigger_Event();
                GameEventManager.Raise(e);
            }
        }
    }

    public void OnInteract()
    {
        //Press Q - currently picks up item
        if (PlayerHeldObject.Instance.holdsItem)
        {
            PlayerHeldObject.Instance.PutDownObject();
            return;
        } 
        
        if (currentTarget == null) return;
        currentTarget.OnInteract();
    }

    public void OnInspect()
    {
        if (currentTarget == null) return;
        
        //Press E - currently gets dialogue infos
        currentTarget.InitDialogue();
    }
}
