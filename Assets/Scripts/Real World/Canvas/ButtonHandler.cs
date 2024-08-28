using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonHandler : MonoBehaviour
{
    public GameObject box1; // Referensi ke box 1
    public GameObject box2; // Referensi ke box 2
    public GameObject box3; // Referensi ke box 3
    public Button investasiButton; // Referensi ke tombol Investasi
    public Button playButton; // Referensi ke tombol Play

    private bool isBox3Active = false; // Track if box3 is active

    private void Start()
    {
        if (investasiButton != null)
        {
            investasiButton.onClick.AddListener(OnInvestasiButtonClick);
        }
        else
        {
            Debug.LogError("Investasi Button not assigned in the Inspector.");
        }

        if (playButton != null)
        {
            // Pastikan tombol Play aktif saat awal permainan
            playButton.gameObject.SetActive(true);
        }
    }

    public void OnInvestasiButtonClick()
    {
        if (box1 != null && box2 != null && box3 != null)
        {
            // Debug sebelum mengubah status box3
            Debug.Log("Box3 active in hierarchy before change: " + box3.activeInHierarchy);

            // Aktifkan tombol Play jika berpindah dari box2 ke box3
            if (box2.activeInHierarchy && playButton != null)
            {
                playButton.gameObject.SetActive(false);
            }

            // Nonaktifkan box2
            box2.SetActive(false);

            // Aktifkan box3 dan atur posisinya sama dengan box2
            box3.SetActive(true);
            box3.transform.position = box2.transform.position;
            box3.transform.rotation = box2.transform.rotation;

            // Update the status of box3
            isBox3Active = true;

            // Pastikan tombol Play aktif jika berpindah dari box1 ke box2
            if (box1.activeInHierarchy && playButton != null)
            {
                playButton.gameObject.SetActive(true);
            }

            // Debug setelah mengubah status box3
            Debug.Log("Box3 active in hierarchy after change: " + box3.activeInHierarchy);
            Debug.Log("Box2 active in hierarchy after change: " + box2.activeInHierarchy);
        }
        else
        {
            Debug.LogError("Box1, Box2, or Box3 not assigned in the Inspector.");
        }
    }

    private void Update()
    {
        // Start coroutine to re-enable Play button if 'S' key is pressed and box3 is active
        if (isBox3Active && Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(EnablePlayButtonAfterDelay(1f)); // Wait for 1 second
        }
    }

    private IEnumerator EnablePlayButtonAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (playButton != null)
        {
            playButton.gameObject.SetActive(true);
        }
    }
}
