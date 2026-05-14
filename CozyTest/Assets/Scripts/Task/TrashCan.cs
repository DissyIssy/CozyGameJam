using System.Collections;
using System.Collections.Generic;
using GameEvents.Manager;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] private Transform holeCenter;
    [SerializeField] private float suckSpeed = 5f;
    
    //ADD SUCK IN DEBUG LOGS SINCE NO SUCKING IN HAPPENS
    private void OnTriggerEnter(Collider other)
    {
        //Works with layers, TrashCanLayer and TrashLayer (physics matrix)
        if (TaskManager.Instance.taskStarted)
        {
            StartCoroutine(SuckIntoVoid(other.gameObject));
        }
    }

    IEnumerator SuckIntoVoid(GameObject trash)
    {
        // Disable physics so it doesn't fight the animation
        if (trash.TryGetComponent<Rigidbody>(out var rb)) rb.isKinematic = true;
        if (trash.TryGetComponent<Collider>(out var col)) col.enabled = false;

        while (trash != null && Vector3.Distance(trash.transform.position, holeCenter.position) > 0.1f)
        {
            // Move towards center
            trash.transform.position = Vector3.MoveTowards(
                trash.transform.position, 
                holeCenter.position, 
                suckSpeed * Time.deltaTime
            );

            // Shrink it down
            trash.transform.localScale = Vector3.Lerp(
                trash.transform.localScale, 
                Vector3.zero, 
                suckSpeed * Time.deltaTime
            );

            yield return null;
        }

        Destroy(trash);
        
        // Call Event
        OneTrashCollected_Event e = new OneTrashCollected_Event();
        GameEventManager.Raise(e);
    }
}
