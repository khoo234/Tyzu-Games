using UnityEngine;

public class SitTrigger : MonoBehaviour
{
    public Animator playerAnimator; // Animator on the player
    private bool isInTrigger = false; // Track if player is in the trigger area
    public Transform sitPosition; // Desired position and rotation for sitting
    private bool hasSat = false; // Track if the player has already sat down

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger area.");
            isInTrigger = true; // Player is in trigger area
            playerAnimator = other.GetComponent<Animator>(); // Get Animator from player
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited the trigger area.");
            isInTrigger = false; // Player left the trigger area
            hasSat = false; // Reset sit action when player leaves

            // Ensure player stands up when leaving the trigger area
            if (playerAnimator != null)
            {
                Debug.Log("Exiting trigger, setting 'duduk' to false.");
                playerAnimator.SetBool("duduk", false); // Stop sit animation
            }
        }
    }

    private void Update()
    {
        if (isInTrigger && !hasSat && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed. Triggering sit animation.");
            if (playerAnimator != null)
            {
                // Set player position and rotation before sitting
                PositionPlayerForSitting();

                // Start sit animation
                playerAnimator.SetBool("duduk", true);

                hasSat = true; // Mark that the player has sat down
            }
        }
    }

    private void PositionPlayerForSitting()
    {
        if (playerAnimator != null && sitPosition != null)
        {
            Transform playerTransform = playerAnimator.transform;

            // Set player's position to the desired sitting position
            playerTransform.position = sitPosition.position;

            // Set player's rotation to match the target rotation
            playerTransform.rotation = sitPosition.rotation;
        }
    }
}
