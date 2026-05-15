using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHeldObject : MonoBehaviour
{
    public static PlayerHeldObject Instance;
    [SerializeField] private Transform handSocket;
    [HideInInspector] public bool holdsItem;
    [HideInInspector] public LightItem lightItemScript;
    private GameObject currentObject;

    private void Awake()
    {
        Instance = this;
    }

    public static void PickUpObject(GameObject itemObject)
    {
        if (Instance.holdsItem)
        { 
            //Puts item down if already holding one
            Instance.PutDownObject();
        }
        
        Instance.currentObject = itemObject;
        Instance.lightItemScript = itemObject.GetComponentInChildren<LightItem>();
            
        //Disables physics
        if (itemObject.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
        }

        //Disables colliders
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

    public void PutDownObject()
    {
        if (!Instance.holdsItem || Instance.currentObject == null)
        {
            return;
        }
        
        if (Instance.holdsItem)
        {
            Instance.currentObject.transform.SetParent(null);
            
            //Enable colliders
            foreach (var col in Instance.currentObject.GetComponentsInChildren<Collider>())
            {
                col.enabled = true;
            }
            
            //Enable physics
            if (Instance.currentObject.TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = false;
            }
            
            Instance.currentObject = null;
            Instance.lightItemScript = null;
            Instance.holdsItem = false;
        }
    }

    public void DeleteObject()
    {
        Destroy(currentObject);
    }
}
