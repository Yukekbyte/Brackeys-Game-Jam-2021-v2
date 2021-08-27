using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public PlayerMovement mov;
    public bool previewing;
    void Update()
    {
        
        if (previewing)
        {
            mov.enabled = false;

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                transform.position += new Vector3(-10f * Time.deltaTime,0f,0f);
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                transform.position += new Vector3(10f * Time.deltaTime,0f,0f);
        }
        else
        {
            mov.enabled = true;
            Vector3 vec = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            transform.position = vec;
        }
    }
}
