using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileDetect : MonoBehaviour
{
    Collider2D currentTile;
    Collider2D leftTile;
    Collider2D rightTile;
    Collider2D upTile;
    Collider2D downTile;
    Collider2D prevTile;
    public PlayerMovement mov;

    //specific tile related variables
    bool poisoned;
    int poisonCount;

    //This fuction should get called every time the player ARRIVES at a new tile
    public void UpdateTiles()
    {
        //Reset movement restrictions
        mov.goleft = true;
        mov.goright = true;
        mov.goup = true;
        mov.godown = true;

    
        //Detect adjecent tiles + previoustile

        prevTile = currentTile;
        currentTile = Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y));// Current point
        leftTile = Physics2D.OverlapPoint(new Vector2(transform.position.x - 1, transform.position.y));// Left point
        rightTile = Physics2D.OverlapPoint(new Vector2(transform.position.x + 1, transform.position.y));// Right point
        upTile = Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y + 1));// Up point
        downTile = Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y - 1));// Down point


        //Check the walls and restrict movement if needed

        if(leftTile.CompareTag("Wall"))
            mov.goleft = false;
        if(rightTile.CompareTag("Wall"))
            mov.goright = false;
        if(upTile.CompareTag("Wall"))
            mov.goup = false;
        if(downTile.CompareTag("Wall"))
            mov.godown = false;



        //Enemy Tile
        if(currentTile.CompareTag("Enemy") && !prevTile.CompareTag("Weapons"))
        {
            print("dead by enemy");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        //Finish Tile
        if(currentTile.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        //Poison Tile
        if(poisoned)
        {
            poisonCount -= 1;
            print(poisonCount);
        }
        if(currentTile.CompareTag("Poison"))
        {
            poisoned = true;
            poisonCount = 3; // amount of tiles you can travel before dying
        }
        if(poisonCount < 0)
        {
            print("dead by poison");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Antidote Tile
        if(currentTile.CompareTag("Antidote"))
        {
            poisoned = false;
        }
        
        //INSERT LOGIC OF ALL THE FOLLOWING TILES
    }
}
