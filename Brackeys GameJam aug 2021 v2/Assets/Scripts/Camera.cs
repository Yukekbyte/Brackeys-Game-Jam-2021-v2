using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        transform.position = vec;
    }
}
