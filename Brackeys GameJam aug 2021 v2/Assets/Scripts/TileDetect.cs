using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileDetect : MonoBehaviour
{
    Collider currentTile;
    Collider leftTile;
    Collider rightTile;
    Collider upTile;
    Collider downTile;
    Collider prevTile;
    public PlayerMovement mov;

    //specific tile related variables
    bool poisoned;
    int poisonCount = 1;
    bool weapons;

    public void UpdateTiles() //This fuction gets called the frame the player ARRIVES at a new tile
    {
        //Reset movement restrictions
        mov.goleft = true;
        mov.goright = true;
        mov.goup = true;
        mov.godown = true;

    
        // Detects adjecent tiles and puts them into an array (array of one if there is a tile, empty array if not)
        prevTile = currentTile;
        Collider[] currentTileArray = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 1.2f, transform.position.z), 0.1f); // Current cube
        Collider[] leftTileArray = Physics.OverlapSphere(new Vector3(transform.position.x - 1f, transform.position.y - 1f, transform.position.z), 0.1f); // Left cube
        Collider[] rightTileArray = Physics.OverlapSphere(new Vector3(transform.position.x + 1f, transform.position.y - 1f, transform.position.z), 0.1f); // Right cube
        Collider[] upTileArray = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z + 1f), 0.1f); // Up cube
        Collider[] downTileArray = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z - 1f), 0.1f); // Down cube

        //if there is a tile in the spot, store it in an object
        if(currentTileArray.Length > 0)
            currentTile = currentTileArray[0];
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (leftTileArray.Length > 0)
            leftTile = leftTileArray[0];
        if (rightTileArray.Length > 0)
            rightTile = rightTileArray[0];
        if (upTileArray.Length > 0)
            upTile = upTileArray[0];
        if (downTileArray.Length > 0)
            downTile = downTileArray[0];
        print("prev: " + prevTile.tag + "   cur: " + currentTile.tag);

        //Check the edges and restrict movement if needed
        if(leftTileArray.Length == 0)
            mov.goleft = false;
        if(rightTileArray.Length == 0)
            mov.goright = false;
        if(upTileArray.Length == 0)
            mov.goup = false;
        if(downTileArray.Length == 0)
            mov.godown = false;

        //Weapons Tile

        if(currentTile.CompareTag("Weapons"))
            weapons = true;

        //Enemy Tile
        if(currentTile.CompareTag("Enemy") && !weapons)
        {
            print("dead by enemy");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        //Finish Tile
        if(currentTile.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        //Ice Tile
        if(currentTile.CompareTag("Ice"))
        {
            mov.MoveDir(currentTile.transform.position - prevTile.transform.position);
        }
    }

    public void UpdateWhenInput() // This fucntion gets called the frame the player tries to move
    {
        //Antidote Tile
        if(currentTile.CompareTag("Antidote"))
        {
            poisoned = false;
        }

        //Poison Tile
        if(poisoned)
        {
            poisonCount -= 1;
        }

        if(currentTile.CompareTag("Poison") && !poisoned)
        {
            poisoned = true;
            poisonCount = 3; // amount of tiles you can travel before dying
        }
        if(poisonCount <= 0)
        {
            print("dead by poison");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
