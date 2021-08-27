using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrig : MonoBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponentsInChildren<MeshRenderer>()[0].enabled = false;
        gameObject.GetComponentsInChildren<MeshRenderer>()[1].enabled = true;
    }

    public void TurnOn()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        gameObject.GetComponentsInChildren<MeshRenderer>()[0].enabled = true;
        gameObject.GetComponentsInChildren<MeshRenderer>()[1].enabled = false;
    }
}
