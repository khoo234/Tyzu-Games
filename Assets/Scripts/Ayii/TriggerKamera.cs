using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TriggerKamera : MonoBehaviour
{
    public GantiKamera Script; // Referensi ke script CameraChange
    public GameObject gameInGameUI; // Referensi ke UI untuk game di dalam game
    public float cameraSwitchDelay = 1.0f; // Delay untuk switch kamera

    private bool isInTrigger = false;
    private bool hasPressedE = false; // Variabel untuk memantau apakah E sudah ditekan
    private bool canPressS = false; // Variabel untuk mengontrol apakah tombol S bisa ditekan
    private PlayerInput playerInput; // Referensi ke PlayerInput
    public UnityEvent Masuk;

    private void Start()
    {
        // Temukan PlayerInput pada objek dengan tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInput = player.GetComponent<PlayerInput>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = true;
            hasPressedE = false; // Reset ketika memasuki trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInTrigger = false;
            hasPressedE = false; // Reset ketika keluar dari trigger

            // Pindah ke third-person view jika keluar dari trigger
            if (Script != null)
            {
                Script.SwitchToThirdPerson();
            }

            // Sembunyikan UI game di dalam game
            if (gameInGameUI != null)
            {
                gameInGameUI.SetActive(false);
                Cursor.visible = false; // Sembunyikan cursor mouse
                Cursor.lockState = CursorLockMode.Locked; // Kunci cursor di tengah layar
            }

            // Aktifkan kembali input pemain jika sebelumnya dinonaktifkan
            if (playerInput != null)
            {
                playerInput.enabled = true;
            }
        }
    }

    private void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Tombol E ditekan dan berada di dalam trigger
            if (!hasPressedE) // "Do Once" functionality
            {
                Masuk?.Invoke();
                hasPressedE = true; // Mark as done to prevent re-triggering
                StartCoroutine(SwitchCameraAfterDelay(cameraSwitchDelay)); // Panggil coroutine dengan delay
                StartCoroutine(ShowGameInGameUIImmediately()); // Tampilkan UI segera

                // Nonaktifkan input pemain saat berada dalam trigger
                if (playerInput != null)
                {
                    playerInput.enabled = false;
                }
            }
        }

        // Periksa apakah tombol S ditekan hanya jika canPressS adalah true
        if (isInTrigger && Input.GetKeyDown(KeyCode.S) && canPressS)
        {
            if (playerInput != null)
            {
                playerInput.enabled = true; // Aktifkan kembali input pemain
            }
        }
    }

    private IEnumerator SwitchCameraAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Tunggu selama delay
        if (Script != null)
        {
            Script.SwitchToFirstPerson(); // Ganti ke first-person view
        }
    }

    private IEnumerator ShowGameInGameUIImmediately()
    {
        // Tampilkan UI game di dalam game segera
        if (gameInGameUI != null && hasPressedE)
        {
            gameInGameUI.SetActive(true);
            Cursor.visible = true; // Tampilkan cursor mouse
            Cursor.lockState = CursorLockMode.None; // Lepaskan cursor dari tengah layar
        }

        // Yield return null to satisfy IEnumerator requirement
        yield return null;
    }


    // Metode untuk mengatur status canPressS
    public void SetCanPressS(bool value)
    {
        canPressS = value;
    }

    // Metode untuk mengembalikan status isInTrigger
    public bool IsInTrigger()
    {
        return isInTrigger;
    }

    // Metode untuk mengembalikan status hasPressedE
    public bool HasPressedE()
    {
        return hasPressedE;
    }

    // Metode untuk mengatur status hasPressedE
    public void SetHasPressedE(bool value)
    {
        hasPressedE = value;
    }
}
