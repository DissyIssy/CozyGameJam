using System;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Serialization;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private bool debugModeCamera;
    [SerializeField] private CinemachineCamera virtualCamera;
    private static CameraSwitcher currentActive;

    public void SetActive()
    {
        if (currentActive != null && currentActive != this)
        {
            currentActive.virtualCamera.Priority = 10;
        }

        virtualCamera.Priority = 20;
        currentActive = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DebugPrint("Current Camera:" + currentActive);
            SetActive();
        }
    }

    private void DebugPrint(string text)
    {
        if (debugModeCamera)
        {
            Debug.Log(text);
        }
    }
}
