using System;
using UnityEngine;
using Yarn.Unity;

public class ItemDialogueChecks : MonoBehaviour
{
    private bool debugMode;
    
    [YarnFunction("HasItem")]
    public static bool HasItem(string itemName)
    {
        
        if (!PlayerHeldObject.Instance.holdsItem)
        {
            return false;
        }
        
        string heldItemName = PlayerHeldObject.Instance.lightItemScript.interactableInfo.displayName;
        return string.Equals(heldItemName, itemName, StringComparison.OrdinalIgnoreCase);
    }
    
    [YarnCommand("DeleteHeldItem")]
    public static void DeleteHeldItem()
    {
        if (PlayerHeldObject.Instance != null && PlayerHeldObject.Instance.holdsItem)
        {
            PlayerHeldObject.Instance.DeleteObject();
        }
    }
    
    [YarnFunction("IsHoldingSomething")]
    public static bool IsHoldingSomething()
    {
        return PlayerHeldObject.Instance != null && PlayerHeldObject.Instance.holdsItem;
    }
}
