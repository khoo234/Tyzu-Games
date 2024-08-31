using System.Collections;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject ThirdCam; // Kamera third-person
    public GameObject FirstCam; // Kamera first-person
    public int CamMode; // Mode kamera (0: Third-Person, 1: First-Person)
    public float breatheIntensity = 0.1f; // Intensitas gerakan bernafas
    public float breatheSpeed = 0.5f; // Kecepatan perubahan gerakan bernafas
    public float transitionDuration = 1.0f; // Durasi transisi kamera
    public float cameraSwitchDelay = 1.0f; // Delay sebelum kamera switch (public)
    public Transform player; // Objek pemain yang ingin dirotasi

    private Vector3 thirdPersonPosition = new Vector3(0.2f, 1.375f, -4f);
    private float thirdPersonFOV = 40f;

    private Vector3 firstPersonPosition = new Vector3(0.2f, 1.18f, 0.161f);
    private float firstPersonFOV = 60f;

    private Coroutine breatheCoroutine;
    private Coroutine transitionCoroutine;

    private CameraTrigger cameraTriggerScript;

    void Start()
    {
        cameraTriggerScript = FindObjectOfType<CameraTrigger>();

        // Set posisi awal untuk kedua kamera
        if (ThirdCam.activeSelf)
        {
            ThirdCam.GetComponent<Camera>().fieldOfView = thirdPersonFOV;
            ThirdCam.transform.localPosition = thirdPersonPosition;
        }
        else
        {
            FirstCam.GetComponent<Camera>().fieldOfView = firstPersonFOV;
            FirstCam.transform.localPosition = firstPersonPosition;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && cameraTriggerScript.IsInTrigger() && !cameraTriggerScript.HasPressedE())
        {
            cameraTriggerScript.SetHasPressedE(true); // Tandai bahwa E telah ditekan
            StartCoroutine(DelayedCameraSwitch(cameraSwitchDelay)); // Mulai coroutine dengan delay
        }
    }

    private IEnumerator DelayedCameraSwitch(float delay)
    {
        // Tunggu sesuai dengan delay
        yield return new WaitForSeconds(delay);

        // Toggle cam mode
        if (CamMode == 1)
        {
            CamMode = 0;
            StartCoroutine(SwitchCamera(FirstCam, ThirdCam, firstPersonPosition, thirdPersonPosition, firstPersonFOV, thirdPersonFOV));
        }
        else
        {
            CamMode = 1;
            StartCoroutine(SwitchCamera(ThirdCam, FirstCam, thirdPersonPosition, firstPersonPosition, thirdPersonFOV, firstPersonFOV));
        }

        // Rotasi pemain agar menghadap ke depan
        RotatePlayerToFront();
    }

    public void SwitchToFirstPerson()
    {
        if (CamMode == 0) // Jika saat ini di third-person
        {
            CamMode = 1;
            StartCoroutine(SwitchCamera(ThirdCam, FirstCam, thirdPersonPosition, firstPersonPosition, thirdPersonFOV, firstPersonFOV));
            RotatePlayerToFront();
        }
    }

    public void SwitchToThirdPerson()
    {
        if (CamMode == 1) // Jika saat ini di first-person
        {
            CamMode = 0;
            StartCoroutine(SwitchCamera(FirstCam, ThirdCam, firstPersonPosition, thirdPersonPosition, firstPersonFOV, thirdPersonFOV));
            RotatePlayerToFront();
        }
    }

    private IEnumerator SwitchCamera(GameObject fromCam, GameObject toCam, Vector3 fromPosition, Vector3 toPosition, float fromFOV, float toFOV)
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }

        if (breatheCoroutine != null)
        {
            StopCoroutine(breatheCoroutine);
        }

        toCam.SetActive(true);
        fromCam.SetActive(false);

        float elapsedTime = 0f;

        // Store the initial rotation of the camera
        Quaternion fromRotation = fromCam.transform.rotation;
        Quaternion toRotation = toCam.transform.rotation;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            toCam.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, t);
            toCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(fromFOV, toFOV, t);

            // Smoothly interpolate rotation
            toCam.transform.rotation = Quaternion.Slerp(fromRotation, toRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Final adjustments
        toCam.transform.localPosition = toPosition;
        toCam.GetComponent<Camera>().fieldOfView = toFOV;
        toCam.transform.rotation = toRotation;

        breatheCoroutine = StartCoroutine(BreatheEffect(toCam.transform));
    }

    private void RotatePlayerToFront()
    {
        if (player != null)
        {
            // Rotate player to face forward
            player.rotation = Quaternion.Euler(0, player.rotation.eulerAngles.y, 0);
        }
    }

    private IEnumerator BreatheEffect(Transform cameraTransform)
    {
        Vector3 originalPos = cameraTransform.localPosition;
        while (true)
        {
            float xOffset = Mathf.Sin(Time.time * breatheSpeed) * breatheIntensity;
            float yOffset = Mathf.Cos(Time.time * breatheSpeed) * breatheIntensity;
            cameraTransform.localPosition = originalPos + new Vector3(xOffset, yOffset, 0);
            yield return null;
        }
    }
}
