using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CubetesInteraction1 : MonoBehaviour
{
    public string cubetesType;
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInput = player.GetComponent<PlayerInput>();
        }

        if (interactionCanvas != null)
        {
            interactionCanvas.gameObject.SetActive(false);
        }

        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterials = objectRenderer.materials;
        }

        if (investmentButton != null)
        {
            investmentButton.onClick.AddListener(OnInvestmentButtonClick);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            hasInteracted = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            if (playerInput != null)
            {
                playerInput.enabled = true;
            }

            if (interactionCanvas != null)
            {
                interactionCanvas.gameObject.SetActive(false);
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

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
