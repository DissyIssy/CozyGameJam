using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.Serialization;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private bool debugModeCamera;
    [FormerlySerializedAs("activeCamera")]
    [SerializeField] private CinemachineCamera virtualCamera;
    private static CameraSwitcher currentActive;

    private void Awake()
    {
        if (virtualCamera == null)
        {
            virtualCamera = GetComponentInChildren<CinemachineCamera>();
        }
    }

    public void SetActive()
    {
        if (virtualCamera == null)
        {
            DebugPrint("CameraSwitcher virtualCamera is not assigned.");
            return;
        }

        if (currentActive != null && currentActive != this)
        {
            if (currentActive.virtualCamera != null)
            {
                currentActive.virtualCamera.Priority = 10;
            }
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
