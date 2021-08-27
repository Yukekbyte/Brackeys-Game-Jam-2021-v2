using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrig : MonoBehaviour
{
    bool on;

    void Awake()
    {
        on = gameObject.GetComponent<Collider>().enabled;
    }
    public void TurnOn()
    {
        gameObject.GetComponent<Collider>().enabled = !on;
        gameObject.GetComponentsInChildren<MeshRenderer>()[0].enabled = !on;
        gameObject.GetComponentsInChildren<MeshRenderer>()[1].enabled = on;
        on = !on;
    }
}
