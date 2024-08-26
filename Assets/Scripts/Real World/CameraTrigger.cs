using System.Collections;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public CameraChange cameraChangeScript; // Referensi ke script CameraChange
    public GameObject gameInGameUI; // Referensi ke UI untuk game di dalam game
    public float delayBeforeUIActive = 2.0f; // Delay sebelum UI aktif
    public Renderer boxRenderer; // Referensi ke Renderer box
    public Color newBoxColor = Color.red; // Warna baru untuk box setelah delay

    private bool isInTrigger = false;
    private bool hasPressedE = false; // Variabel untuk memantau apakah E sudah ditekan

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Masuk trigger. Press E untuk first person view.");
            isInTrigger = true;
            hasPressedE = false; // Reset ketika memasuki trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Keluar trigger. Switch third-person otomatis.");
            isInTrigger = false;
            hasPressedE = false; // Reset ketika keluar dari trigger

            // Pindah ke third-person view jika keluar dari trigger
            cameraChangeScript.SwitchToThirdPerson();

            // Sembunyikan UI game di dalam game
            if (gameInGameUI != null)
            {
                gameInGameUI.SetActive(false);
                Cursor.visible = false; // Sembunyikan cursor mouse
                Cursor.lockState = CursorLockMode.Locked; // Kunci cursor di tengah layar
            }

            // Kembalikan warna material box
            if (boxRenderer != null)
            {
                boxRenderer.material.color = Color.white; // Kembalikan warna box ke default
            }
        }
    }

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

    private void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Tombol E ditekan dan berada di dalam trigger
            hasPressedE = true;
            StartCoroutine(ShowGameInGameUIAfterDelay());
        }
    }

    private IEnumerator ShowGameInGameUIAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeUIActive); // Tunggu selama delay

        // Tampilkan UI game di dalam game jika tombol E ditekan
        if (gameInGameUI != null && hasPressedE)
        {
            gameInGameUI.SetActive(true);
            Cursor.visible = true; // Tampilkan cursor mouse
            Cursor.lockState = CursorLockMode.None; // Lepaskan cursor dari tengah layar
        }

        // Ubah warna material box
        if (boxRenderer != null)
        {
            boxRenderer.material.color = newBoxColor; // Set warna baru
        }
    }
}
