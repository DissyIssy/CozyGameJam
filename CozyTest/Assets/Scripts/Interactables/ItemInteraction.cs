using UnityEngine;
using Yarn.Unity;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] private SO_InteractableInfo interactableInfo;
    [SerializeField] private DialogueRunner dialogueRunner;

    public void InitDialogue()
    {
        dialogueRunner.StartDialogue(interactableInfo.yarnID_Interactable);
    }
    
    public void StopDialouge()
    {
        if (dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.Stop();
        }
    }
}
