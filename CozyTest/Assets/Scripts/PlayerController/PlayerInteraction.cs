using System;
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
            GameEventManager.Raise(e);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out InteractableBase interactable))
        {
            currentTarget.StopDialogue();
            currentTarget = null;
            
            //Send out event for UI
            ExitedInteractableTrigger_Event e = new ExitedInteractableTrigger_Event();
            GameEventManager.Raise(e);
        }
    }

    public void OnInteract()
    {
        if (currentTarget == null) return;
        
        currentTarget.InitDialogue();
        currentTarget.OnInteract();
    }
}
