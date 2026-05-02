using System;
using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private CinemachineCamera activeCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activeCamera.Priority = 1;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            activeCamera.Priority = 0;
        }
    }
}
