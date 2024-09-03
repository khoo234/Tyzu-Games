using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class InteraksiTombol : MonoBehaviour
{
    [Header("Button")]
    public Button Button1;
    public Button Button2;

    private void Start()
    {
        Tombol1();
    }

    public void Tombol1()
    {
        Button1.interactable = false;
        Button2.interactable = true;
        EventSystem.current.SetSelectedGameObject(Button1.gameObject);
    }
    public void Tombol2()
    {
        Button2.interactable = false;
        Button1.interactable = true;
        EventSystem.current.SetSelectedGameObject(Button2.gameObject);
    }
}
