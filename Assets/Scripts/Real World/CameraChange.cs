using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject ThirdCam;
    public GameObject FirstCam;
    public int CamMode;
    public float breatheIntensity = 0.1f; // Intensitas gerakan bernafas
    public float breatheSpeed = 0.5f; // Kecepatan perubahan gerakan bernafas
    public float transitionDuration = 1.0f; // Durasi transisi kamera

    private Vector3 thirdPersonPosition = new Vector3(0.2f, 1.375f, -4f);
    private float thirdPersonFOV = 40f;

    private Vector3 firstPersonPosition = new Vector3(0.2f, 1.375f, 0.47f);
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
        }
    }

    public void SwitchToFirstPerson()
    {
        if (CamMode == 0) // Jika saat ini di third-person
        {
            CamMode = 1;
            StartCoroutine(SwitchCamera(ThirdCam, FirstCam, thirdPersonPosition, firstPersonPosition, thirdPersonFOV, firstPersonFOV));
        }
    }

    public void SwitchToThirdPerson()
    {
        if (CamMode == 1) // Jika saat ini di first-person
        {
            CamMode = 0;
            StartCoroutine(SwitchCamera(FirstCam, ThirdCam, firstPersonPosition, thirdPersonPosition, firstPersonFOV, thirdPersonFOV));
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

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
            toCam.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, t);
            toCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(fromFOV, toFOV, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        toCam.transform.localPosition = toPosition;
        toCam.GetComponent<Camera>().fieldOfView = toFOV;

        breatheCoroutine = StartCoroutine(BreatheEffect(toCam.transform));
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
