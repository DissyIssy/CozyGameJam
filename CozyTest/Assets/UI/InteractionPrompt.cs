using System;
using GameEvents.Manager;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractionPrompt : MonoBehaviour
{
    [SerializeField] private GameObject InspectText;
    [SerializeField] private GameObject PickUpText;
    [SerializeField] private float gapBetweenPrompts = 0.5f;
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
        if (targetToFollow == null) return;
        
        if (InspectText != null && InspectText.activeSelf)
        {
            InspectText.transform.position = targetToFollow.position + currentOffset;
        }

        if (PickUpText != null && PickUpText.activeSelf)
        {
            Vector3 verticalGap = new Vector3(0, gapBetweenPrompts, 0);
            PickUpText.transform.position = targetToFollow.position + currentOffset + verticalGap;
        }
    }

    void OnEnteredInteractableTriggerEvent(EnteredInteractableTrigger_Event e)
    {
        if (InspectText != null)
        {
            targetToFollow = e.interactableTransform;
            currentOffset = e.offset;
            InspectText.SetActive(true);
        }

        if (PickUpText != null && e.isPickUpAble)
        {
            targetToFollow = e.interactableTransform;
            currentOffset = e.offset;
            PickUpText.SetActive(true);
        }
    }

    void OnExitedInteractableTriggerEvent(ExitedInteractableTrigger_Event e)
    {
        if (InspectText != null)
        {
            InspectText.SetActive(false);
        }
        
        if (PickUpText != null)
        {
            PickUpText.SetActive(false);
        }
        
        targetToFollow = null;
    }
}
