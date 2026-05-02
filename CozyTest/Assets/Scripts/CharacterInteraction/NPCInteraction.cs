using System;
using UnityEngine;
using Yarn.Unity;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private SO_PassengerInfo passengerInfo;
    [SerializeField] private DialogueRunner dialogueRunner;
    

    public void InitDialogue()
    {
        dialogueRunner.StartDialogue(passengerInfo.yarnID);
    }
}