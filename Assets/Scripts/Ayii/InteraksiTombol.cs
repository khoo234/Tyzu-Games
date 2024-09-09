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
    public Button Button3;
    public bool TombolNyala;

    private void Start()
    {
        if (TombolNyala)
        {
            Tombol1();
        }
    }

    public void Tombol1()
    {
        Button1.interactable = false;
        Button2.interactable = true;
        if (Button3 != null)
        {
            Button3.interactable = true;
        }
        EventSystem.current.SetSelectedGameObject(Button1.gameObject);
    }
    public void Tombol2()
    {
        Button2.interactable = false;
        Button1.interactable = true;
        if (Button3 != null)
        {
            Button3.interactable = true;
        }
        EventSystem.current.SetSelectedGameObject(Button2.gameObject);
    }
    public void Tombol3()
    {
        if (Button3 != null)
        {
            Button3.interactable = false;
        }
        Button1.interactable = true;
        Button2.interactable = true;
        EventSystem.current.SetSelectedGameObject(Button3.gameObject);
    }
    public void Reset()
    {
        Button2.interactable = true;
        Button1.interactable = true;
        if(Button3 != null)
        {
            Button3.interactable = true;
        }
    }
}
