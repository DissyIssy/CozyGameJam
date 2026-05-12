using UnityEngine;

public class PlayerHeldObject : MonoBehaviour
{
    public static PlayerHeldObject Instance;
    [SerializeField] private Transform handSocket;
    private bool holdsItem;

    private void Awake()
    {
        Instance = this;
    }

    public static void PickUpObject(GameObject itemObject)
    {
        if (Instance.holdsItem)
        {
            return;
        }
        
        //Deactivates physics
        if (itemObject.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }

        foreach (var col in itemObject.GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }
        
        itemObject.transform.SetParent(Instance.handSocket);
        
        //Change transforms relative to the hand
        itemObject.transform.localPosition = Vector3.zero;
        itemObject.transform.localRotation = Quaternion.identity;

        Instance.holdsItem = true;
    }
}
