using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Animasi : MonoBehaviour
{
    public GameObject uiElement;
    public float animationDuration = 0.5f;
    public float delayDuration = 2.0f;

    private RectTransform uiRectTransform;

    public Vector3 scaleAwal = new Vector3(0.1f, 0.1f, 1f);
    public Vector3 scaleAkhir = new Vector3(1f, 1f, 1f);
    public Vector3 scaleOvershoot = new Vector3(1.2f, 1.2f, 1f);

    void Start()
    {
        uiRectTransform = uiElement.GetComponent<RectTransform>();
    }

    public void ShowUI()
    {
        StopAllCoroutines();
        uiElement.SetActive(true);
        StartCoroutine(Muncul(uiRectTransform, animationDuration, scaleAwal, scaleOvershoot, scaleAkhir));
    }

    public void HideUI()
    {
        StopAllCoroutines();
        uiElement.SetActive(true);
        StartCoroutine(Hilang(uiRectTransform, animationDuration, scaleAkhir, scaleOvershoot, scaleAwal, () => uiElement.SetActive(false)));
    }

    private System.Collections.IEnumerator Muncul(RectTransform rectTransform, float duration, Vector3 startScale, Vector3 overshootScale, Vector3 endScale)
    {
        float elapsedTime = 0;
        float halfDuration = duration / 2;

        while (elapsedTime < halfDuration)
        {
            rectTransform.localScale = Vector3.Lerp(startScale, overshootScale, (elapsedTime / halfDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;

        while (elapsedTime < halfDuration)
        {
            rectTransform.localScale = Vector3.Lerp(overshootScale, endScale, (elapsedTime / halfDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = endScale;
    }

    private System.Collections.IEnumerator Hilang(RectTransform rectTransform, float duration, Vector3 startScale, Vector3 overshootScale, Vector3 endScale, UnityAction onComplete)
    {
        float elapsedTime = 0;
        float halfDuration = duration / 2;

        while (elapsedTime < halfDuration)
        {
            rectTransform.localScale = Vector3.Lerp(startScale, overshootScale, (elapsedTime / halfDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;

        while (elapsedTime < halfDuration)
        {
            rectTransform.localScale = Vector3.Lerp(overshootScale, endScale, (elapsedTime / halfDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = endScale;
        onComplete?.Invoke();
    }
}
