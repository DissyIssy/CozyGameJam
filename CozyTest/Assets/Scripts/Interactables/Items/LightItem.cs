using UnityEngine;

public class LightItem : ItemInteraction
{

    public override void OnInteract()
    {
        if (interactableInfo.isPickUpAble)
        {
            GameObject objectToCarry = transform.parent != null ? transform.parent.gameObject : gameObject;
            PlayerHeldObject.PickUpObject(objectToCarry);
        }
    }
}
