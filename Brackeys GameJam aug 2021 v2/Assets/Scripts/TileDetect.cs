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
    public EffectsAndItems effectsAndItems;

    //specific tile related variables
    

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
            effectsAndItems.weapon = true;

        //Enemy Tile
        if(currentTile.CompareTag("Enemy"))
        {
            if (!effectsAndItems.weapon)
            {
                print("dead by enemy");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                
            }
            else
            {
                effectsAndItems.weapon = false;
            }
           
        }
        
        //Finish Tile
        if(currentTile.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        //Ice Tile
        if(currentTile.CompareTag("Ice"))
        {
            if (effectsAndItems.wet && !effectsAndItems.frost)
            {
                effectsAndItems.frost = true;
                effectsAndItems.frostCount = 3;
            }
            mov.MoveDir(currentTile.transform.position - prevTile.transform.position);
            mov.sliding = true; //this bool makes sure that the tiles keep updating when sliding over ice
        }
        else mov.sliding = false;

        //Water Tile
        if (currentTile.CompareTag("Water"))
        {
            effectsAndItems.wet = true;
            effectsAndItems.wetCount = 3;
            effectsAndItems.gunpowder = false;
            if (effectsAndItems.electricity)
            {
                print("dead by electrocution");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        //Fire Tile
        if (currentTile.CompareTag("Fire"))
        {
            effectsAndItems.wet = false;
            effectsAndItems.frost = false;
            if (effectsAndItems.gunpowder)
            {
                print("dead by explosion");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        //Electricity Tile
        if (currentTile.CompareTag("Electricity"))
        {
            effectsAndItems.electricity = true;
        }
    }

    public void UpdateWhenInput() // This fucntion gets called the frame the player tries to move
    {
        //Antidote Tile
        if(currentTile.CompareTag("Antidote"))
        {
            effectsAndItems.poison = false;
        }

        //Frost counter countdown
        if (effectsAndItems.frost)
        {
            effectsAndItems.frostCount -= 1;
        }
        if (effectsAndItems.frostCount <= 0)
        {
            print("dead by cold");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Wet counter countdown
        if (effectsAndItems.wet)
        {
            effectsAndItems.wetCount -= 1;
        }
        if (effectsAndItems.wetCount <= 0)
        {
            effectsAndItems.wet = false;
            effectsAndItems.wetCount = 1;
        }

        //Poison Tile
        if (effectsAndItems.poison)
        {
            effectsAndItems.poisonCount -= 1;
        }

        if(currentTile.CompareTag("Poison") && !effectsAndItems.poison)
        {
            effectsAndItems.poison = true;
            effectsAndItems.poisonCount = 3; // amount of tiles you can travel before dying
        }
        if(effectsAndItems.poisonCount <= 0)
        {
            print("dead by poison");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
