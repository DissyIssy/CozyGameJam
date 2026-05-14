using System;
using UnityEngine;
using Yarn.Unity;
using GameEvents.Manager;

public class NPCInteraction : InteractableBase
{
    [SerializeField] private SO_PassengerInfo passengerInfo;
    [SerializeField] Enum_NPCState NPCState;


    private void OnEnable()
    {
        GameEventManager.AddListener<TaskFinished_Event>(OnReportBack);
    }

    private void OnDisable()
    {
        GameEventManager.RemoveListener<TaskFinished_Event>(OnReportBack);
    }

    public override void InitDialogue()
    {
        switch (NPCState)
        {
            case Enum_NPCState.New:
                dialogueRunner.StartDialogue(passengerInfo.yarnID_New);
                NPCState = Enum_NPCState.Normal;
                break;
            case Enum_NPCState.TaskGiving:
                dialogueRunner.StartDialogue(passengerInfo.yarnID_TaskGiving);
                TaskManager.Instance.StartTask();
                NPCState = Enum_NPCState.Normal;
                break;
            case Enum_NPCState.Normal:
                dialogueRunner.StartDialogue(passengerInfo.yarnID_Normal);
                break;
            case Enum_NPCState.off:
                dialogueRunner.StartDialogue(passengerInfo.yarnID_Off);
                break;
            case Enum_NPCState.gone:
                dialogueRunner.StartDialogue(passengerInfo.yarnID_Gone);
                break;
            case Enum_NPCState.TaskReporting:
                dialogueRunner.StartDialogue(passengerInfo.yarnID_TaskReporting);
                NPCState = Enum_NPCState.Normal;
                break;
            default:
                Debug.LogWarning("NPC has no state");
                break;
        }
    }

    public void OnReportBack(TaskFinished_Event e)
    {
        NPCState = Enum_NPCState.TaskReporting;
    }
}