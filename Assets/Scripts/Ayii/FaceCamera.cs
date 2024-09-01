using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera Camera;
    public Vector3 rotationOffset;

    void Start()
    {
        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        if (cameraObject != null)
        {
            Camera = cameraObject.GetComponent<Camera>();
            transform.rotation *= Quaternion.Euler(rotationOffset);
        }
    }

    void Update()
    {
        if (Camera != null)
        {
            transform.LookAt(Camera.transform);
        }
    }
}
