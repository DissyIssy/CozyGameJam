using System;
using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] private bool debugModeTeleport;
    //External reference - should not happen, use eventbus toot toot
    [SerializeField] private CharacterController characterController;
    
    [SerializeField] private Transform destination;

    private bool isArrival = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isArrival)
        {
            DebugPrint("Player entered teleport trigger.");
            
            PlayerTeleporter destinationScript = destination.GetComponent<PlayerTeleporter>();
            if (destinationScript != null)
            {
                destinationScript.isArrival = true;
            }

            StartCoroutine(TeleportPlayer(other.gameObject));
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isArrival = false;
        }
    }

    private IEnumerator TeleportPlayer(GameObject player)
    {
        CharacterController characterController = player.GetComponent<CharacterController>();

        if (characterController != null)
        {
            characterController.enabled = false;
        }

        player.transform.position = destination.position;
        player.transform.rotation = destination.rotation;
        
        DebugPrint("Teleporting player to:" + destination.position);

        yield return new WaitForFixedUpdate();

        if (characterController != null)
        {
            this.characterController.enabled = true;
        }
    }
    
    private void DebugPrint(string text)
    {
        if (debugModeTeleport)
        {
            Debug.Log(text, this);
        }
    }
}
