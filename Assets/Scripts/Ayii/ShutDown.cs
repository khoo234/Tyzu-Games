using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Import untuk Button

public class ShutDown : MonoBehaviour
{
    public GantiKamera Script; // Referensi ke skrip CameraChange
    public GameObject canvas; // Referensi ke Canvas UI
    public GameObject panelInvestasi; // Referensi ke Panel Investasi
    public float moveBackDistance = 2.0f; // Jarak mundur karakter
    public float moveSpeed = 5.0f; // Kecepatan gerakan mundur

    private void Start()
    {
        panelInvestasi.SetActive(false);
    }

    public void ShutDownClick()
    {
        Script.SwitchToThirdPerson();
        canvas.SetActive(false);
        panelInvestasi.SetActive(false);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            CharacterController characterController = player.GetComponent<CharacterController>();
            if (characterController != null)
            {
                Vector3 moveBack = -player.transform.forward * moveBackDistance;
                StartCoroutine(MoveBackCoroutine(characterController, moveBack));
            }
        }
    }

    private IEnumerator MoveBackCoroutine(CharacterController characterController, Vector3 moveBack)
    {
        float elapsedTime = 0f;
        float moveDuration = moveBackDistance / moveSpeed;
        Vector3 startPosition = characterController.transform.position;

        while (elapsedTime < moveDuration)
        {
            characterController.transform.position = Vector3.Lerp(startPosition, startPosition + moveBack, (elapsedTime / moveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        characterController.transform.position = startPosition + moveBack;
    }
}
