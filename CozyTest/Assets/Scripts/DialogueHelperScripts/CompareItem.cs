using UnityEngine;
using Yarn.Unity;

public class CompareItem : MonoBehaviour
{
    private bool debugMode;
    [YarnFunction("has_item")]
    public static bool HasItem(string itemName)
    {
        
        if (!PlayerHeldObject.Instance.holdsItem)
        {
            return false;
        }
        
        string heldItemName = PlayerHeldObject.Instance.lightItemScript.interactableInfo.displayName;

        if (string.Equals(heldItemName,itemName,System.StringComparison.OrdinalIgnoreCase))
        {
            PlayerHeldObject.Instance.DeleteObject();
            return true;
        }

        return false;
    }
}
