using System;
using GameEvents.Manager;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class PlayerInteraction : MonoBehaviour
{
    private NPCInteraction currentNPC;
    private ItemInteraction currentItem;
    private PlayerInputActions playerInputActions;

    //You can surely combine the character and item interaction into a single function
    //Also be aware in the case that both cases are true.
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.CharacterControls.Enable();
        playerInputActions.CharacterControls.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        playerInputActions.CharacterControls.Interact.performed -= OnInteract;
        playerInputActions.CharacterControls.Disable();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform targetTransform = null;
        
        if (other.TryGetComponent(out NPCInteraction npc))
        {
            currentNPC = npc;
            //Send out event for UI
            EnteredInteractableTrigger_Event e = new EnteredInteractableTrigger_Event();
            e.interactableTransform = npc.transform;
            GameEventManager.Raise(e);
        }

        if (other.TryGetComponent(out ItemInteraction item))
        {
            currentItem = item;
            //Send out event for UI
            EnteredInteractableTrigger_Event e = new EnteredInteractableTrigger_Event();
            e.interactableTransform = item.transform;
            GameEventManager.Raise(e);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out NPCInteraction npc))
        {
            if (npc == currentNPC)
            {
                currentNPC.StopDialouge();
                currentNPC = null;
                
                //Send out event for UI
                ExitedInteractableTrigger_Event e = new ExitedInteractableTrigger_Event();
                GameEventManager.Raise(e);
            }
        }

        if (other.TryGetComponent(out ItemInteraction item))
        {
            if (item == currentItem)
            {
                currentItem.StopDialouge();
                currentItem = null;
                
                //Send out event for UI
                ExitedInteractableTrigger_Event e = new ExitedInteractableTrigger_Event();
                GameEventManager.Raise(e);
            }
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (currentNPC != null)
        {
            currentNPC.InitDialogue();
            return;
        }

        if (currentItem != null)
        {
            currentItem.InitDialogue();
            currentItem.PickUp();
        }
    }
}
