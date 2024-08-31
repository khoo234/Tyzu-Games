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
        // Pastikan panel investasi tidak aktif saat awal
        if (panelInvestasi != null)
        {
            panelInvestasi.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel Investasi not assigned in the Inspector.");
        }

        // Pastikan tombol Play aktif saat awal
        if (playButton != null)
        {
            playButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Play Button not assigned in the Inspector.");
        }

        // Pastikan tombol Investasi aktif saat awal
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
        // Mengubah kamera ke third-person
        if (cameraChangeScript != null)
        {
            cameraChangeScript.SwitchToThirdPerson();
        }
        else
        {
            Debug.LogError("CameraChange script not assigned in the Inspector.");
        }

        // Menyembunyikan canvas
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
        else
        {
            Debug.LogError("Canvas not assigned in the Inspector.");
        }

        // Memindahkan karakter mundur
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            CharacterController characterController = player.GetComponent<CharacterController>();
            if (characterController != null)
            {
                Vector3 moveBack = -player.transform.forward * moveBackDistance;
                StartCoroutine(MoveBackCoroutine(characterController, moveBack));
            }
            else
            {
                Debug.LogError("CharacterController component is missing on the player.");
            }
        }
        else
        {
            Debug.LogError("Player not found in the scene.");
        }

        // Reset Panel Investasi
        if (panelInvestasi != null)
        {
            panelInvestasi.SetActive(false);
        }

        // Reset tombol Play
        if (playButton != null)
        {
            playButton.gameObject.SetActive(true);
        }

        // Reset tombol Investasi
        if (investasiButton != null)
        {
            investasiButton.gameObject.SetActive(true);
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
