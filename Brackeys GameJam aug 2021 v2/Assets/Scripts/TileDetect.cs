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
    public string prevTileDir; //this will store the direction the previous tile is from the player: left / up / right / down
    PlayerMovement mov;
    EffectsAndItems effectsAndItems;
    GameObject Canvas;

    //specific tile related variables
    
    void Awake()
    {
        mov = GetComponent<PlayerMovement>();
        effectsAndItems = GetComponent<EffectsAndItems>();
        Canvas = GameObject.Find("Canvas");
    }
    public void UpdateTiles() //This fuction gets called the frame the player ARRIVES at a new tile
    {
        //Reset movement restrictions
        mov.goleft = true;
        mov.goright = true;
        mov.goup = true;
        mov.godown = true;

    
        // Detects adjecent tiles and puts them into an array (array of one if there is a tile, empty array if not)
        prevTile = currentTile;
        Collider[] currentTileArray = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), 0.1f); // Current cube
        Collider[] leftTileArray = Physics.OverlapSphere(new Vector3(transform.position.x - 1f, transform.position.y - 1f, transform.position.z), 0.1f); // Left cube
        Collider[] rightTileArray = Physics.OverlapSphere(new Vector3(transform.position.x + 1f, transform.position.y - 1f, transform.position.z), 0.1f); // Right cube
        Collider[] upTileArray = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z + 1f), 0.1f); // Up cube
        Collider[] downTileArray = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z - 1f), 0.1f); // Down cube

        //if there is a tile in the spot, store it in an object
        if(currentTileArray.Length > 0)
            currentTile = currentTileArray[0];
        else
            {
                mov.speed = 10;
                mov.Fall();
                Invoke("Respawn", 1.5f);
            }
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
                Canvas.GetComponent<Menus>().DisableDeathText();
                Canvas.GetComponent<Menus>().EnemieDeath.SetActive(true);


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
                effectsAndItems.wet = false;
                effectsAndItems.frostCount = 4;
            }
            mov.MoveDir(currentTile.transform.position - prevTile.transform.position);
            mov.sliding = true;
        }
        else mov.sliding = false;

        //Water Tile
        if (currentTile.CompareTag("Water"))
        {
            
            effectsAndItems.wet = true;
            effectsAndItems.wetCount = 4;
            effectsAndItems.gunpowder = false;
            if (effectsAndItems.electricity)
            {
                Canvas.GetComponent<Menus>().DisableDeathText();
                Canvas.GetComponent<Menus>().ElecDeath.SetActive(true);
            }
        }
        //Fire Tile
        if (currentTile.CompareTag("Fire"))
        {
            effectsAndItems.wet = false;
            effectsAndItems.frost = false;
            if (effectsAndItems.gunpowder)
            {
             
                Canvas.GetComponent<Menus>().DisableDeathText();
                Canvas.GetComponent<Menus>().ExplosionDeath.SetActive(true);
            }
        }
        //Gunpowder Tile
        if (currentTile.CompareTag("Gunpowder"))
        {
            effectsAndItems.gunpowder = true;
        }
        //Electricity Tile
        if (currentTile.CompareTag("Electricity"))
        {
            effectsAndItems.electricity = true;
        }
        //Poison Tile
        if (currentTile.CompareTag("Poison") && !effectsAndItems.poison)
        {
            effectsAndItems.poison = true;
            effectsAndItems.poisonCount = 4; // amount of tiles you can travel before dying
        }
        //Antidote Tile
        if(currentTile.CompareTag("Antidote"))
        {
            effectsAndItems.poison = false;
        }
        //Timebomb Tile
        if(prevTile.CompareTag("Timebomb") && effectsAndItems.gunpowder)
        {
            //make sure the player can't walk back on the cloud
            if(prevTileDir == "left")   mov.goleft = false;
            if(prevTileDir == "right")  mov.goright = false;
            if(prevTileDir == "up")     mov.goup = false;
            else                        mov.godown = false;
            //destroy cloud
            Destroy(prevTile.gameObject);
            effectsAndItems.gunpowder = false;
        }

        //Cloud Tile
        if(prevTile.CompareTag("Cloud"))
        {
            //make sure the player can't walk back on the cloud
            if(prevTileDir == "left")   mov.goleft = false;
            if(prevTileDir == "right")  mov.goright = false;
            if(prevTileDir == "up")     mov.goup = false;
            else                        mov.godown = false;
            //destroy cloud
            Destroy(prevTile.gameObject);
        }
        if(currentTile.CompareTag("Cannon") && effectsAndItems.gunpowder)
        {
            mov.MoveDir(currentTile.gameObject.transform.GetChild(0).position - currentTile.transform.position);
            effectsAndItems.gunpowder = false;
            mov.Invoke("TilesUpdatedToFalse",0.4f);
        }
    }

    public void UpdateWhenInput() // This fucntion gets called the frame the player tries to move
    {
        

        //Frost counter countdown
        if (effectsAndItems.frost)
        {
            effectsAndItems.frostCount -= 1;
        }
        if (effectsAndItems.frostCount <= 0)
        {
            Canvas.GetComponent<Menus>().DisableDeathText();
            Canvas.GetComponent<Menus>().FrostDeath.SetActive(true);
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

        
        if(effectsAndItems.poisonCount <= 0)
        {
            Canvas.GetComponent<Menus>().DisableDeathText();
            Canvas.GetComponent<Menus>().PoisonDeath.SetActive(true);
        }
    }
}
