using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Eventtt : MonoBehaviour
{
    public UnityEvent On;
    public UnityEvent Off;

    public void Nyala()
    {
        On?.Invoke();
    }
    public void Mati()
    {
        Off?.Invoke();
    }
}
