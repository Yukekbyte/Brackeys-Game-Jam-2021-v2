using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform movePoint;
    public TileDetect tileDetect;
    public float speed;
    bool tilesUpdated = false;
    

    //Movement bools
    public bool goleft = true;
    public bool goright = true;
    public bool goup = true;
    public bool godown = true;
    void Start()
    {
        movePoint.parent = null;
    }

    void Update()
    {
        //Player position follows movepoint position
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);

        //MovePoint position change
        if(Vector3.Distance(transform.position, movePoint.position) <= 0.05f) //if player is on movepoint (if player arrives at new tile), new input is allowed
        {
            //Updates tiles only once when arriving at new tile
            if(tilesUpdated == false)
            {
                tileDetect.UpdateTiles();
                tilesUpdated = true;
            }

            //Horizontal input
            if(Input.GetKeyDown(KeyCode.A) && goleft)
            {
                movePoint.position += new Vector3(-1f, 0f, 0f);
                tilesUpdated = false;
                tileDetect.UpdateWhenInput();
            }
            else if(Input.GetKeyDown(KeyCode.D) && goright)
            {
                movePoint.position += new Vector3(1f, 0f, 0f);
                tilesUpdated = false;
                tileDetect.UpdateWhenInput();
            }

            //Vertical input
            else if(Input.GetKeyDown(KeyCode.W) && goup)
            {
                movePoint.position += new Vector3(0f, 0f, 1f);
                tilesUpdated = false;
                tileDetect.UpdateWhenInput();
            }
            else if(Input.GetKeyDown(KeyCode.S) && godown)
            {
                movePoint.position += new Vector3(0f, 0f, -1f);
                tilesUpdated = false;
                tileDetect.UpdateWhenInput();
            }
        }
    }

    public void MoveDir(Vector3 dir)
    {
        movePoint.position += dir;
        tilesUpdated = false;
        tileDetect.UpdateWhenInput();
    }
}