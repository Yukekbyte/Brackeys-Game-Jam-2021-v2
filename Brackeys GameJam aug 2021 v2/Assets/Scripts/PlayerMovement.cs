using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform movePoint;
    TileDetect tileDetect;
    Animator animator;
    public float speed;
    bool tilesUpdated = false;
    

    //Movement bools
    public bool goleft = true;
    public bool goright = true;
    public bool goup = true;
    public bool godown = true;

    //Tiledetect related
    public bool sliding;

    void Awake()
    {
        animator = GetComponent<Animator>();
        tileDetect = GetComponent<TileDetect>();
    }
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
                if(!sliding) //when over ice, the tiles keep updating until the ice sliding is over
                    tilesUpdated = true;
            }

            //Horizontal input
            if(Input.GetKeyDown(KeyCode.A) && goleft)
            {
                //transform.rotation = 
                movePoint.position += new Vector3(-1f, 0f, 0f);
                tilesUpdated = false;
                tileDetect.UpdateWhenInput();
                animator.SetTrigger("Walktrigger");
            }
            else if(Input.GetKeyDown(KeyCode.D) && goright)
            {
                movePoint.position += new Vector3(1f, 0f, 0f);
                tilesUpdated = false;
                tileDetect.UpdateWhenInput();
                animator.SetTrigger("Walktrigger");
            }

            //Vertical input
            else if(Input.GetKeyDown(KeyCode.W) && goup)
            {
                movePoint.position += new Vector3(0f, 0f, 1f);
                tilesUpdated = false;
                tileDetect.UpdateWhenInput();
                animator.SetTrigger("Walktrigger");
            }
            else if(Input.GetKeyDown(KeyCode.S) && godown)
            {
                movePoint.position += new Vector3(0f, 0f, -1f);
                tilesUpdated = false;
                tileDetect.UpdateWhenInput();
                animator.SetTrigger("Walktrigger");
            }
        }
    }

    public void MoveDir(Vector3 dir)
    {
        movePoint.position += dir;
    }
}