using UnityEngine;

[CreateAssetMenu(fileName = "New Interactable", menuName = "CozyEndtimes/New Interactable")]
public class SO_InteractableInfo : ScriptableObject
{
    public string interactableName;
    public string yarnID_Interactable;
    public string yarnID_PickUpDialogue;
    public bool isPickUpAble;
    //Could also have a cool sound here
}
