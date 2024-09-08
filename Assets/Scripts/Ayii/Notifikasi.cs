using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Notifikasi : MonoBehaviour
{
    public GameObject Notif;
    public float animationDuration = 0.5f;
    public float delayDuration = 2.0f;

    private RectTransform uiRectTransform;

    public Vector3 scaleAwal = new Vector3(0.1f, 0.1f, 1f);
    public Vector3 scaleAkhir = new Vector3(1f, 1f, 1f);
    public Vector3 scaleOvershoot = new Vector3(1.2f, 1.2f, 1f);

    void Start()
    {
        uiRectTransform = Notif.GetComponent<RectTransform>();
    }

    public void ShowPopup()
    {
        StopAllCoroutines();
        Notif.SetActive(true);
        StartCoroutine(ShowAndHide());
    }

    private System.Collections.IEnumerator ShowAndHide()
    {
        yield return StartCoroutine(Muncul(uiRectTransform, animationDuration, scaleAwal, scaleOvershoot, scaleAkhir));

        yield return new WaitForSeconds(delayDuration);

        yield return StartCoroutine(Hilang(uiRectTransform, animationDuration, scaleAkhir, scaleOvershoot, scaleAwal, () => Notif.SetActive(false)));
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
