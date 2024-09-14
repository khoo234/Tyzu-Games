using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Unity.VisualScripting;

public class AnimasiPosisi : MonoBehaviour
{
    public GameObject uiElement;
    public float animationDuration = 0.5f;
    public float delayDuration = 2.0f;

    private RectTransform uiRectTransform;

    public Vector3 startPosition = new Vector3(-500f, 0f, 0f);
    public Vector3 overshootPosition = new Vector3(50f, 0f, 0f);
    public Vector3 endPosition = new Vector3(0f, 0f, 0f);

    public bool PopUp = false;
    private bool isShowing = false;
    public bool Musuh;
    private Darah DarahMusuh;
    public TMP_Text KhususMusuh;
    public int koinnya;
    public UnityEvent Sound;

    void Start()
    {
        uiRectTransform = uiElement.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Musuh && DarahMusuh != null)
        {
            koinnya = DarahMusuh.CoinMiaw;
            if (KhususMusuh != null)
            {
                KhususMusuh.text = koinnya + " Coin";
            }
        }
        FindEnemyHealth();
    }

    private void FindEnemyHealth()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy != null)
        {
            DarahMusuh = enemy.GetComponent<Darah>();
        }
    }

    public void ShowUI()
    {
        if (!isShowing)
        {
            StopAllCoroutines();
            isShowing = true;
            uiElement.SetActive(true);
            StartCoroutine(Muncul(uiRectTransform, animationDuration, startPosition, overshootPosition, endPosition));
        }
    }

    public void ShowUIWithDelay()
    {
        ShowUI();
        if (PopUp)
        {
            Sound?.Invoke();
            StartCoroutine(WaitAndHide());
        }
    }

    public void HideUI()
    {
        if (isShowing)
        {
            StopAllCoroutines();
            StartCoroutine(Hilang(uiRectTransform, animationDuration, endPosition, overshootPosition, startPosition, () =>
            {
                uiElement.SetActive(false);
                isShowing = false;
            }));
        }
    }

    private System.Collections.IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(delayDuration);
        HideUI();
    }

    private System.Collections.IEnumerator Muncul(RectTransform rectTransform, float duration, Vector3 startPos, Vector3 overshootPos, Vector3 endPos)
    {
        float elapsedTime = 0;
        float halfDuration = duration / 2;

        while (elapsedTime < halfDuration)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(startPos, overshootPos, (elapsedTime / halfDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;

        while (elapsedTime < halfDuration)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(overshootPos, endPos, (elapsedTime / halfDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = endPos;
    }

    private System.Collections.IEnumerator Hilang(RectTransform rectTransform, float duration, Vector3 startPos, Vector3 overshootPos, Vector3 endPos, UnityAction onComplete)
    {
        float elapsedTime = 0;
        float halfDuration = duration / 2;

        while (elapsedTime < halfDuration)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(startPos, overshootPos, (elapsedTime / halfDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;

        while (elapsedTime < halfDuration)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(overshootPos, endPos, (elapsedTime / halfDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = endPos;
        onComplete?.Invoke();
    }
}
