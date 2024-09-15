using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LadangTrigger : MonoBehaviour
{
    public UnityEvent MasukTrigger;
    public UnityEvent KeluarTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MasukTrigger?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KeluarTrigger?.Invoke();
        }
    }
}
