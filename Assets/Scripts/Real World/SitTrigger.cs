using UnityEngine;
using UnityEngine.Events;

public class SitTrigger : MonoBehaviour
{
    public Animator playerAnimator;
    public bool DalamTrigger = false;
    public Transform sitPosition;
    public bool Duduk = false;

    [Header ("Masuk / Keluar Trigger")]
    public UnityEvent Masuk;
    public UnityEvent Keluar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DalamTrigger = true;
            playerAnimator = other.GetComponent<Animator>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DalamTrigger = false;
            Duduk = false;

            if (playerAnimator != null)
            {
                playerAnimator.SetBool("duduk", false);
            }
        }
    }

    private void Update()
    {
        if (DalamTrigger && !Duduk)
        {
            Masuk?.Invoke();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (playerAnimator != null)
                {
                    PositionPlayerForSitting();

                    playerAnimator.SetBool("duduk", true);

                    Duduk = true;
                }
            }
        }
        else if(DalamTrigger && Duduk)
        {
            Keluar?.Invoke();
        }
        else
        {
            Keluar?.Invoke();
        }
    }

    private void PositionPlayerForSitting()
    {
        if (playerAnimator != null && sitPosition != null)
        {
            Transform playerTransform = playerAnimator.transform;

            playerTransform.position = sitPosition.position;

            playerTransform.rotation = sitPosition.rotation;
        }
    }
}
