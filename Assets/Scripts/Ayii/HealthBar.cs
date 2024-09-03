using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header ("Info Darah")]
    [SerializeField] private Slider healthBar;

    [Header ("Perubahan Warna Darah")]
    public Color Low;
    public Color High;

    public void SetHealth(int health, int maxHealthh)
    {
        healthBar.gameObject.SetActive(health < maxHealthh);
        healthBar.value = health;
        healthBar.maxValue = maxHealthh;
        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, healthBar.normalizedValue);
    }

    public void SetupHealthBar(Canvas canvas, Camera camera)
    {
        healthBar.transform.SetParent(canvas.transform, false);
        if (healthBar.TryGetComponent<FaceCamera>(out FaceCamera faceCamera))
        {
            faceCamera.Camera = camera;
        }
    }
}
