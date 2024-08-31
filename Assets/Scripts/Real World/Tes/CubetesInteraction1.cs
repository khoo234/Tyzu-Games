using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // Pastikan namespace ini tersedia
using UnityEngine.UI; // Pastikan namespace ini tersedia untuk Canvas dan UI

public class CubetesInteraction1 : MonoBehaviour
{
    public string cubetesType; // "Menanam" untuk Cubetes 1, "Shopping" untuk Cubetes 2
    private bool playerInside = false; // Status apakah player berada di dalam cubetes
    private bool hasInteracted = false; // Status apakah player sudah melakukan interaksi
    private PlayerInput playerInput; // Referensi ke PlayerInput
    public Canvas interactionCanvas; // Referensi ke Canvas yang akan ditampilkan
    public Material newMaterial; // Material baru untuk diterapkan saat tombol "E" ditekan
    public Material investmentMaterial; // Material baru untuk diterapkan saat tombol "Investasi" ditekan
    private Material[] originalMaterials; // Menyimpan material asli
    private Renderer objectRenderer; // Referensi ke Renderer objek
    public Button investmentButton; // Referensi ke tombol investasi pada Canvas

    public float uiDelay = 1.0f; // Delay sebelum UI aktif

    private void Start()
    {
        // Temukan PlayerInput pada objek dengan tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInput = player.GetComponent<PlayerInput>();
        }

        // Pastikan Canvas dimulai dalam keadaan tidak aktif
        if (interactionCanvas != null)
        {
            interactionCanvas.gameObject.SetActive(false);
        }

        // Ambil Renderer dari objek ini dan simpan material aslinya
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterials = objectRenderer.materials;
        }

        // Setup button click listener
        if (investmentButton != null)
        {
            investmentButton.onClick.AddListener(OnInvestmentButtonClick);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true; // Tandai bahwa player masuk
            hasInteracted = false; // Reset interaksi saat player masuk
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

            // Nonaktifkan Canvas jika pemain keluar dari trigger
            if (interactionCanvas != null)
            {
                interactionCanvas.gameObject.SetActive(false);
            }

            // Nonaktifkan kursor mouse
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // Kembalikan material ke kondisi semula
            if (objectRenderer != null && originalMaterials != null)
            {
                objectRenderer.materials = originalMaterials;
            }
        }
    }

    private void Update()
    {
        // Periksa apakah tombol E ditekan dan interaksi belum terjadi
        if (Input.GetKeyDown(KeyCode.E) && playerInside && !hasInteracted)
        {
            Debug.Log(cubetesType); // Tampilkan pesan sesuai dengan jenis cubetes

            // Tandai bahwa interaksi telah terjadi
            hasInteracted = true;

            // Nonaktifkan input pemain
            if (playerInput != null)
            {
                playerInput.enabled = false;
            }

            // Mulai coroutine untuk delay UI
            if (interactionCanvas != null)
            {
                StartCoroutine(ShowUICoroutine());
            }

            // Aktifkan kursor mouse
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Ubah material objek
            if (objectRenderer != null && newMaterial != null)
            {
                // Ganti material kedua jika ada lebih dari satu material
                Material[] materials = objectRenderer.materials;
                if (materials.Length > 1)
                {
                    materials[1] = newMaterial;
                    objectRenderer.materials = materials;
                }
            }
        }
    }

    private IEnumerator ShowUICoroutine()
    {
        // Tunggu sesuai dengan delay
        yield return new WaitForSeconds(uiDelay);

        // Aktifkan Canvas setelah delay
        interactionCanvas.gameObject.SetActive(true);
    }

    private void OnInvestmentButtonClick()
    {
        // Ganti material dengan material investasi
        if (objectRenderer != null && investmentMaterial != null)
        {
            Material[] materials = objectRenderer.materials;
            if (materials.Length > 1)
            {
                materials[1] = investmentMaterial;
                objectRenderer.materials = materials;
            }
        }
    }
}
