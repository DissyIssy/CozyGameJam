using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Transform camTransform;

    void Start() => camTransform = Camera.main.transform;

    void LateUpdate()
    {
        // Makes the UI face the camera every frame
        transform.LookAt(transform.position + camTransform.rotation * Vector3.forward,
                         camTransform.rotation * Vector3.up);
    }
}
