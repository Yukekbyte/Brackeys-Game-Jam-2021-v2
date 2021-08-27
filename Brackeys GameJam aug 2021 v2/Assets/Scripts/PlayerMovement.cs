using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform movePoint;
    TileDetect tileDetect;
    Animator animator;
    AudioManager audioManager;
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
        audioManager = FindObjectOfType<AudioManager>();
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
                if(!sliding)
                    tilesUpdated = true;
            }

            //Horizontal input
            if((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && goleft)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90, transform.eulerAngles.z); //Rotation of character
                movePoint.position += new Vector3(-1f, 0f, 0f);     //Moves the movepoint (not the character)
                tilesUpdated = false;                               //so it doesn't keep updating the tiles until the character arrives at a new tile
                tileDetect.UpdateWhenInput();
                tileDetect.prevTileDir = "right";                   //this updates the direction the current tile (now previous because we will move) is after we have moved
                animator.SetTrigger("Walktrigger");                 //start walk animation
                PlayStep();
            }
            else if((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && goright)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90, transform.eulerAngles.z);
                movePoint.position += new Vector3(1f, 0f, 0f);
                tilesUpdated = false;
                tileDetect.UpdateWhenInput();
                tileDetect.prevTileDir = "left";
                animator.SetTrigger("Walktrigger");
                PlayStep();
            }

            //Vertical input
            else if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && goup)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                movePoint.position += new Vector3(0f, 0f, 1f);
                tilesUpdated = false;
                tileDetect.UpdateWhenInput();
                tileDetect.prevTileDir = "down";
                animator.SetTrigger("Walktrigger");
                PlayStep();
            }
            else if((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && godown)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
                movePoint.position += new Vector3(0f, 0f, -1f);
                tilesUpdated = false;
                tileDetect.UpdateWhenInput();
                tileDetect.prevTileDir = "up";
                animator.SetTrigger("Walktrigger");
                PlayStep();
            }
        }
    }

    public void MoveDir(Vector3 dir)
    {
        movePoint.position += dir;
    }
    void PlayStep()
    {
        float a = Random.Range(-1,2);
        if(a < 0)
        {
            audioManager.Play("Step");
        }
        else if (a < 1)
        {
            audioManager.Play("Step2");
        }
        else
        {
            audioManager.Play("Step3");
        }
    }
    public void Fall()
    {
        movePoint.position += new Vector3(0f, -20f, 0f);
    }
    public void TilesUpdatedToFalse()
    {
        tilesUpdated = false;
    }
}