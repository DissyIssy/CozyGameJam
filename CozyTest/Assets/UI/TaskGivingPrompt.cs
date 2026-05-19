using System;
using GameEvents.Manager;
using UnityEngine;

public class TaskGivingPrompt : MonoBehaviour
{
    [SerializeField] private GameObject taskGivingDisplay;
    [SerializeField] private Vector3 displayOffset = new Vector3(0, 2.0f, 0);

    private Transform targetToFollow;

    private void OnEnable()
    {
        GameEventManager.AddListener<EnteredInteractableTrigger_Event>(OnEnteredInteractableTriggerEvent);
        GameEventManager.AddListener<ExitedInteractableTrigger_Event>(OnExitedInteractableTriggerEvent);
    }

    private void OnDisable()
    {
        GameEventManager.RemoveListener<EnteredInteractableTrigger_Event>(OnEnteredInteractableTriggerEvent);
        GameEventManager.RemoveListener<ExitedInteractableTrigger_Event>(OnExitedInteractableTriggerEvent);
    }

    public void Update()
    {
        if (targetToFollow == null) return;

        if (taskGivingDisplay != null && taskGivingDisplay.activeSelf)
        {
            taskGivingDisplay.transform.position = targetToFollow.position + displayOffset;
        }
    }

    void OnEnteredInteractableTriggerEvent(EnteredInteractableTrigger_Event e)
    {
        Transform interactableTransform = e.interactableTransform;

        if (interactableTransform.TryGetComponent<InteractableBase>(out InteractableBase interactableBase))
        {
            if (taskGivingDisplay != null) taskGivingDisplay.SetActive(false);

            if (interactableBase is NPCInteraction npc && npc.NPCState == Enum_NPCState.TaskGiving)
            {
                targetToFollow = e.interactableTransform;
                taskGivingDisplay.SetActive(true);
            }
            else
            {
                targetToFollow = null;
            }
        }
    }

    void OnExitedInteractableTriggerEvent(ExitedInteractableTrigger_Event e)
    {
        if (taskGivingDisplay != null)
        {
            taskGivingDisplay.SetActive(false);
        }

        targetToFollow = null;
    }
}