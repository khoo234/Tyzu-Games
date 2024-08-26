using System.Collections; // Untuk IEnumerator
using UnityEngine;
using UnityEngine.UI; // Untuk mengatur cursor visibility

public class InteractWithConsole : MonoBehaviour
{
    public CameraSwitch cameraSwitch; // Referensi ke script CameraSwitch
    public GameObject gameInGameUI; // Referensi ke UI untuk game di dalam game
    public float delayBeforeUIActive = 2.0f; // Delay sebelum UI aktif
    public Renderer boxRenderer; // Referensi ke Renderer box
    public Color newBoxColor = Color.red; // Warna baru untuk box setelah delay

    private bool isNearConsole = false; // Untuk mengecek apakah pemain berada di dekat konsol

    void Update()
    {
        if (isNearConsole && Input.GetKeyDown(KeyCode.E)) // E adalah tombol untuk interaksi
        {
            Debug.Log("Player beralih ke kamera first-person dan mulai bermain game di dalam game.");
            cameraSwitch.ToggleToFirstPerson(); // Beralih ke first-person

            // Mulai Coroutine untuk menampilkan UI dengan delay
            StartCoroutine(ShowGameInGameUIAfterDelay());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearConsole = true;
            Debug.Log("Player mendekati konsol game.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearConsole = false;
            Debug.Log("Player menjauh dari konsol game.");
            cameraSwitch.ToggleToThirdPerson(); // Kembali ke third-person

            // Sembunyikan UI game di dalam game
            if (gameInGameUI != null)
            {
                gameInGameUI.SetActive(false);
                Cursor.visible = false; // Sembunyikan cursor mouse
                Cursor.lockState = CursorLockMode.Locked; // Kunci cursor di tengah layar
            }
        }
    }

    private IEnumerator ShowGameInGameUIAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeUIActive); // Tunggu selama delay

        // Tampilkan UI game di dalam game
        if (gameInGameUI != null)
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
