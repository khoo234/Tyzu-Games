using System.Collections;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CameraChange cameraChangeScript; // Reference to CameraChange script
    public GameObject gameInGameUI; // Reference to the in-game UI
    public float delayBeforeUIActive = 2.0f; // Delay before UI becomes active

    public GameObject box1; // Reference to box 1 (orange)
    public GameObject box2; // Reference to box 2 (yellow)

    private bool isInTrigger = false;
    private bool hasPressedE = false; // Variable to track if E has been pressed
    public Animator an;

    private void Start()
    {
        // Ensure box1 is active and box2 is inactive at the start
        if (box1 != null) box1.SetActive(true);
        if (box2 != null) box2.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger. Press E for first-person view.");
            isInTrigger = true;
            if (hasPressedE)
            {
                // If E was already pressed, ensure box2 is active
                ActivateBox2();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited trigger. Switching to third-person view automatically.");
            isInTrigger = false;
            hasPressedE = false; // Reset when exiting the trigger

            // Switch to third-person view
            cameraChangeScript.SwitchToThirdPerson();

            // Hide in-game UI and manage cursor visibility
            if (gameInGameUI != null)
            {
                gameInGameUI.SetActive(false);
                Cursor.visible = false; // Hide cursor
                Cursor.lockState = CursorLockMode.Locked; // Lock cursor to the center of the screen
            }

            // Re-activate box1 and deactivate box2
            DeactivateBox2();
        }
    }

    private void Update()
    {   
        if (isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // If E is pressed and player is within the trigger
            hasPressedE = true;
            cameraChangeScript.SwitchToFirstPerson(); // Switch to first-person view
            StartCoroutine(ShowGameInGameUIAfterDelay());
            if (an != null)
            {
                an.SetTrigger("duduk"); // Assuming "Sit" is the trigger parameter for the sitting animation
            }
        }
    }

    private IEnumerator ShowGameInGameUIAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeUIActive); // Wait for the specified delay

        // Show the in-game UI and manage cursor visibility
        if (gameInGameUI != null && hasPressedE)
        {
            gameInGameUI.SetActive(true);
            Cursor.visible = true; // Show cursor
            Cursor.lockState = CursorLockMode.None; // Unlock cursor from the center of the screen
        }

        // Activate box2 and deactivate box1
        ActivateBox2();
    }

    private void ActivateBox2()
    {
        if (box1 != null && box2 != null)
        {
            box2.transform.position = box1.transform.position; // Move box2 to box1's position
            box2.transform.rotation = box1.transform.rotation; // Match box2's rotation with box1
            box1.SetActive(false);
            box2.SetActive(true);
        }
    }

    private void DeactivateBox2()
    {
        if (box1 != null && box2 != null)
        {
            box1.SetActive(true);
            box2.SetActive(false);
            // Ensure box2's position and rotation match box1's
            box2.transform.position = box1.transform.position;
            box2.transform.rotation = box1.transform.rotation;
        }
    }

    // Methods to get and set the trigger status
    public bool IsInTrigger()
    {
        return isInTrigger;
    }

    public bool HasPressedE()
    {
        return hasPressedE;
    }

    public void SetHasPressedE(bool value)
    {
        hasPressedE = value;
    }
}
