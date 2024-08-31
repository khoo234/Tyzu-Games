using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Import untuk Button

public class ShutdownHandler : MonoBehaviour
{
    public CameraChange cameraChangeScript; // Referensi ke skrip CameraChange
    public GameObject canvas; // Referensi ke Canvas UI
    public GameObject panelInvestasi; // Referensi ke Panel Investasi
    public Button playButton; // Referensi ke tombol Play
    public Button investasiButton; // Referensi ke tombol Investasi
    public float moveBackDistance = 2.0f; // Jarak mundur karakter
    public float moveSpeed = 5.0f; // Kecepatan gerakan mundur

    private void Start()
    {
        if (panelInvestasi != null)
        {
            panelInvestasi.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel Investasi not assigned in the Inspector.");
        }

        if (playButton != null)
        {
            playButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Play Button not assigned in the Inspector.");
        }

        if (investasiButton != null)
        {
            investasiButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Investasi Button not assigned in the Inspector.");
        }
    }

    public void OnShutdownButtonClick()
    {
        cameraChangeScript.SwitchToThirdPerson();
        canvas.SetActive(false);
        panelInvestasi.SetActive(false);
        playButton.gameObject.SetActive(true);
        investasiButton.gameObject.SetActive(true);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            CharacterController characterController = player.GetComponent<CharacterController>();
            if (characterController != null)
            {
                Vector3 moveBack = -player.transform.forward * moveBackDistance;
                StartCoroutine(MoveBackCoroutine(characterController, moveBack));
            }
        }
    }

    private IEnumerator MoveBackCoroutine(CharacterController characterController, Vector3 moveBack)
    {
        float elapsedTime = 0f;
        float moveDuration = moveBackDistance / moveSpeed;
        Vector3 startPosition = characterController.transform.position;

        while (elapsedTime < moveDuration)
        {
            characterController.transform.position = Vector3.Lerp(startPosition, startPosition + moveBack, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        characterController.transform.position = startPosition + moveBack;
    }
}
