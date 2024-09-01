using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LayarPC : MonoBehaviour
{
    private bool playerInside = false;
    private bool hasInteracted = false;
    private PlayerInput playerInput;
    public Canvas interactionCanvas;
    public GameObject InvestasiLayar;
    public GameObject PlayLayar;
    public GameObject LayarAsli;

    public float uiDelay = 1.0f;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInput = player.GetComponent<PlayerInput>();
        }

        interactionCanvas.gameObject.SetActive(false);
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
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInside && !hasInteracted)
        {
            hasInteracted = true;

            if (playerInput != null)
            {
                playerInput.enabled = false;
            }

            if (interactionCanvas != null)
            {
                StartCoroutine(ShowUICoroutine());
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private IEnumerator ShowUICoroutine()
    {
        yield return new WaitForSeconds(uiDelay);
        interactionCanvas.gameObject.SetActive(true);
    }

    public void Investasi()
    {
        InvestasiLayar.SetActive(true);
        LayarAsli.SetActive(false);
        PlayLayar.SetActive(false);
    }

    public void Play()
    {
        PlayLayar.SetActive(true);
        InvestasiLayar.SetActive(false);
        LayarAsli.SetActive(false);
    }

    public void KeluarPC()
    {
        LayarAsli.SetActive(true);
        PlayLayar.SetActive(false);
        InvestasiLayar.SetActive(false);
    }
}
