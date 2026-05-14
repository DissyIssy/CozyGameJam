using UnityEngine;
using GameEvents.Manager;

public class EnteredInteractableTrigger_Event : GameEvent
{
    public Transform interactableTransform;
    public Vector3 offset = new Vector3(0, 2f, 0);
    public bool isPickUpAble;
}
