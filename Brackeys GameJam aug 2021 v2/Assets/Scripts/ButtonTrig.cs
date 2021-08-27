using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrig : MonoBehaviour
{
    bool on;

    public void TurnOnOff()
    {
        gameObject.GetComponent<Collider>().enabled = !on;
        gameObject.GetComponent<MeshRenderer>().enabled = !on;
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = on;
        on = !on;
    }
}
