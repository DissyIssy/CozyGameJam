using System;
using GameEvents.Manager;
using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    public GameObject interactionText;
    private Transform targetToFollow;
    private Vector3 currentOffset;

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
        if (interactionText.activeSelf && targetToFollow != null)
        {
            interactionText.transform.position = targetToFollow.position + currentOffset;
        }
    }

    void OnEnteredInteractableTriggerEvent(EnteredInteractableTrigger_Event e)
    {
        if (interactionText != null)
        {
            targetToFollow = e.interactableTransform;
            currentOffset = e.offset;
            interactionText.SetActive(true);
        }
    }

    void OnExitedInteractableTriggerEvent(ExitedInteractableTrigger_Event e)
    {
        if (interactionText != null)
        {
            interactionText.SetActive(false);
            targetToFollow = null;
        }
    }
}
