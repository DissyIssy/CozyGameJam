using System;
using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] private bool debugMode;
    //External reference - should not happen, use eventbus toot toot
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform destination;

    private bool isTeleporting;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTeleporting)
        {
            DebugPrint("Player entered trigger");
            StartCoroutine(TeleportPlayer());
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        }
    }

    private IEnumerator TeleportPlayer()
    {
        isTeleporting = true;
        characterController.enabled = false;
        characterController.transform.position = destination.position;
        yield return new WaitForFixedUpdate();

        characterController.enabled = true;
        isTeleporting = false;
    }
    
    private void DebugPrint(string text)
    {
        if (debugMode)
        {
            Debug.Log(text, this);
        }
    }
}
