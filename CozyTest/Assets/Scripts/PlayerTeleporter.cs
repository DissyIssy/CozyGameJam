using System;
using System.Collections;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] private bool debugModeTeleport;
    [SerializeField] private Transform destination;
    [SerializeField] private float exitYawOffset;

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
        CharacterController playerController = player.GetComponent<CharacterController>();

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        Quaternion destinationRotation = destination.rotation * Quaternion.Euler(0f, exitYawOffset, 0f);

        player.transform.position = destination.position;
        player.transform.rotation = destinationRotation;
        AlignVisualModelRotation(player.transform, destinationRotation);
        
        DebugPrint("Teleporting player to:" + destination.position);

        yield return new WaitForFixedUpdate();

        if (playerController != null)
        {
            playerController.enabled = true;
        }

        ActivateDestinationCamera(player.transform.position);
    }

    private void AlignVisualModelRotation(Transform playerTransform, Quaternion targetRotation)
    {
        Transform mcTransform = playerTransform.Find("MC");
        if (mcTransform != null)
        {
            mcTransform.rotation = targetRotation;
        }
    }

    private void ActivateDestinationCamera(Vector3 playerPosition)
    {
        CameraSwitcher[] switchers = FindObjectsOfType<CameraSwitcher>(true);
        if (switchers == null || switchers.Length == 0)
        {
            return;
        }

        CameraSwitcher insideSwitcher = null;
        CameraSwitcher closestSwitcher = null;
        float closestDistanceSqr = float.MaxValue;

        for (int i = 0; i < switchers.Length; i++)
        {
            CameraSwitcher switcher = switchers[i];
            if (switcher == null)
            {
                continue;
            }

            Collider zoneCollider = switcher.GetComponent<Collider>();
            if (zoneCollider != null && zoneCollider.bounds.Contains(playerPosition))
            {
                insideSwitcher = switcher;
                break;
            }

            float distanceSqr = (switcher.transform.position - playerPosition).sqrMagnitude;
            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                closestSwitcher = switcher;
            }
        }

        CameraSwitcher target = insideSwitcher != null ? insideSwitcher : closestSwitcher;
        if (target != null)
        {
            target.SetActive();
            DebugPrint("Activated camera zone: " + target.name);
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
