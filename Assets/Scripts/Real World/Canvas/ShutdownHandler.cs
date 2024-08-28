using UnityEngine;

public class ShutdownHandler : MonoBehaviour
{
    public CameraChange cameraChangeScript; // Referensi ke skrip CameraChange
    public GameObject canvas; // Referensi ke Canvas UI
    public GameObject box1; // Referensi ke Box 1 (posisi awal)
    public GameObject box2; // Referensi ke Box 2 (posisi yang akan diubah)

    private Vector3 box1InitialPosition; // Posisi awal Box 1
    private Quaternion box1InitialRotation; // Rotasi awal Box 1

    private void Start()
    {
        // Menyimpan posisi dan rotasi awal Box 1
        if (box1 != null)
        {
            box1InitialPosition = box1.transform.position;
            box1InitialRotation = box1.transform.rotation;
        }
        else
        {
            Debug.LogError("Box1 not assigned in the Inspector.");
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

        // Mengatur Box 2 kembali ke posisi dan rotasi awal Box 1
        if (box2 != null && box1 != null)
        {
            // Menonaktifkan box2 untuk menghindari interaksi sebelum mengatur posisinya
            box2.SetActive(false);

            // Mengatur posisi dan rotasi box2 sesuai dengan box1
            box2.transform.position = box1InitialPosition;
            box2.transform.rotation = box1InitialRotation;

            // Mengaktifkan box2 kembali setelah mengatur posisinya
            box2.SetActive(true);
        }
        else
        {
            Debug.LogError("Box1 or Box2 not assigned in the Inspector.");
        }
    }
}
