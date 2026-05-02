using System;
using UnityEngine;
using Yarn.Unity;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private SO_PassengerInfo passengerInfo;
    [SerializeField] private DialogueRunner dialogueRunner;
    [SerializeField] Enum_NPCState NPCState;
    

    public void InitDialogue()
    {
        switch (NPCState)
        {
            case Enum_NPCState.New:
                dialogueRunner.StartDialogue(passengerInfo.yarnID_New);
                NPCState = Enum_NPCState.Normal;
                break;
            case Enum_NPCState.TaskGiving:
                Debug.Log("Not implemented yet");
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
            default:
                Debug.LogWarning("NPC has no state");
                break;
        }
    }

    public void StopDialouge()
    {
        if (dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.Stop();
        }
    }
}