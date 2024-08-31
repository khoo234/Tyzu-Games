using UnityEngine;
using UnityEngine.InputSystem; // Pastikan namespace ini tersedia

public class CubetesInteraction : MonoBehaviour
{
    public string cubetesType; // "Menanam" untuk Cubetes 1, "Shopping" untuk Cubetes 2
    private bool playerInside = false; // Status apakah player berada di dalam cubetes
    private PlayerInput playerInput; // Referensi ke PlayerInput

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
            playerInside = true; // Tandai bahwa player masuk
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false; // Tandai bahwa player keluar

            // Aktifkan kembali input pemain jika pemain keluar dari trigger
            if (playerInput != null)
            {
                playerInput.enabled = true;
            }
        }
    }

    private void Update()
    {
        // Periksa apakah tombol E ditekan
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerInside)
            {
                Debug.Log(cubetesType); // Tampilkan pesan sesuai dengan jenis cubetes

                // Nonaktifkan input pemain
                if (playerInput != null)
                {
                    playerInput.enabled = false;
                }
            }
        }

        // Periksa apakah tombol S ditekan
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (playerInside)
            {
                // Aktifkan kembali input pemain jika tombol S ditekan
                if (playerInput != null)
                {
                    playerInput.enabled = true;
                }
            }
        }
    }
}
