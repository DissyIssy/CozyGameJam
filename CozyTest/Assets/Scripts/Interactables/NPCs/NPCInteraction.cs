using System;
using UnityEngine;
using Yarn.Unity;
using GameEvents.Manager;

public class NPCInteraction : InteractableBase
{
    [SerializeField] private SO_PassengerInfo passengerInfo;
    public Enum_NPCState NPCState;

    public override void InitDialogue()
    {
        switch (NPCState)
        {
            case Enum_NPCState.New:
                dialogueRunner.StartDialogue(passengerInfo.yarnID_New);
                break;
            case Enum_NPCState.Normal:
                dialogueRunner.StartDialogue(passengerInfo.yarnID_Normal);
                break;
            case Enum_NPCState.TaskGiving:
                dialogueRunner.StartDialogue(passengerInfo.yarnID_TaskGiving);
                TaskManager.Instance.StartTask(passengerInfo);
                break;
            case Enum_NPCState.TaskDuring:
                dialogueRunner.StartDialogue(passengerInfo.yarnID_TaskDuring);
                break;
            case Enum_NPCState.TaskReporting:
                dialogueRunner.StartDialogue(passengerInfo.yarnID_TaskReporting);
                break;
            default:
                Debug.LogWarning("NPC has no state");
                break;
        }
    }
}