using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header ("Info Darah")]
    [SerializeField] public Slider healthBar;

    [Header ("Perubahan Warna Darah")]
    public Color Low;
    public Color High;

    private void Start()
    {
        healthBar.gameObject.SetActive(true);
    }

    public void SetHealth(int health, int maxHealthh)
    {
        healthBar.value = health;
        healthBar.maxValue = maxHealthh;
        healthBar.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, healthBar.normalizedValue);
    }
}
