using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private NPCInteraction currentNPC;
    private PlayerInputActions playerInputActions;

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
        if (other.TryGetComponent(out NPCInteraction npc))
        {
            currentNPC = npc;
            Debug.Log("Well, hello there!");
            //Send out event for UI
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out NPCInteraction npc))
        {
            if (npc == currentNPC)
            {
                currentNPC = null;
                Debug.Log("BYE");
                //Send out event for UI
            }
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (currentNPC != null)
        {
            currentNPC.InitDialogue();
        }
    }
}
