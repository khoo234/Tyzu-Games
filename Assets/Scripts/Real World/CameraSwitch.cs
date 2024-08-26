using System.Collections; // For IEnumerator and coroutines
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera thirdPersonCamera; // Third-person camera
    public Camera firstPersonCamera; // First-person camera
    public Transform player; // Player's transform

    private bool isFirstPerson = false;
    public float transitionSpeed = 1.0f; // Speed of the transition
    private Vector3 firstPersonOffset = new Vector3(0, 1.6f, 0.05f); // Offset for the first-person camera

    void Start()
    {
        // Initially, third-person camera is active
        firstPersonCamera.gameObject.SetActive(false);
        thirdPersonCamera.gameObject.SetActive(true);
    }

    public void ToggleToFirstPerson()
    {
        if (!isFirstPerson)
        {
            StartCoroutine(SmoothSwitchToFirstPerson());
            isFirstPerson = true;
        }
    }

    public void ToggleToThirdPerson()
    {
        if (isFirstPerson)
        {
            StartCoroutine(SmoothSwitchToThirdPerson());
            isFirstPerson = false;
        }
    }

    IEnumerator SmoothSwitchToFirstPerson()
    {
        // Smooth transition to first-person
        float elapsedTime = 0f;
        Vector3 thirdPersonCamPos = thirdPersonCamera.transform.position;
        Quaternion thirdPersonCamRot = thirdPersonCamera.transform.rotation;
        Vector3 firstPersonCamPos = player.position + firstPersonOffset;
        Quaternion firstPersonCamRot = player.rotation;

        thirdPersonCamera.gameObject.SetActive(false);
        firstPersonCamera.gameObject.SetActive(true);

        while (elapsedTime < transitionSpeed)
        {
            float t = elapsedTime / transitionSpeed;
            firstPersonCamera.transform.position = Vector3.Lerp(thirdPersonCamPos, firstPersonCamPos, t);
            firstPersonCamera.transform.rotation = Quaternion.Slerp(thirdPersonCamRot, firstPersonCamRot, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        firstPersonCamera.transform.position = firstPersonCamPos;
        firstPersonCamera.transform.rotation = firstPersonCamRot;
    }

    IEnumerator SmoothSwitchToThirdPerson()
    {
        // Smooth transition to third-person
        float elapsedTime = 0f;
        Vector3 firstPersonCamPos = firstPersonCamera.transform.position;
        Quaternion firstPersonCamRot = firstPersonCamera.transform.rotation;
        Vector3 thirdPersonCamPos = thirdPersonCamera.transform.position;
        Quaternion thirdPersonCamRot = thirdPersonCamera.transform.rotation;

        firstPersonCamera.gameObject.SetActive(false);
        thirdPersonCamera.gameObject.SetActive(true);

        while (elapsedTime < transitionSpeed)
        {
            float t = elapsedTime / transitionSpeed;
            thirdPersonCamera.transform.position = Vector3.Lerp(firstPersonCamPos, thirdPersonCamPos, t);
            thirdPersonCamera.transform.rotation = Quaternion.Slerp(firstPersonCamRot, thirdPersonCamRot, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        thirdPersonCamera.transform.position = thirdPersonCamPos;
        thirdPersonCamera.transform.rotation = thirdPersonCamRot;
    }

    void FollowPlayer()
    {
        // Keep first-person camera following the player
        if (isFirstPerson)
        {
            firstPersonCamera.transform.position = player.position + firstPersonOffset;
            firstPersonCamera.transform.rotation = player.rotation;
        }
    }
}

