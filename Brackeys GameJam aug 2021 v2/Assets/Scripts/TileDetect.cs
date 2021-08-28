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
    public int moves;
    public GameObject grassTile;
    AudioManager audioManager;

    //specific tile related variables
    
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        mov = GetComponent<PlayerMovement>();
        effectsAndItems = GetComponent<EffectsAndItems>();
        Canvas = GameObject.Find("Canvas");
        moves = 0;
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
        {
            currentTile = currentTileArray[0];
            mov.falling = false;
        }
        else if(!mov.falling)
        {
            mov.falling = true;
            audioManager.Play("Falling");
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
                audioManager.Play("Enemy");
                Canvas.GetComponent<Menus>().DisableDeathText();
                Canvas.GetComponent<Menus>().EnemieDeath.SetActive(true);
            }
            else
            {
                effectsAndItems.weapon = false;
                Instantiate(grassTile, currentTile.transform.position, currentTile.transform.rotation);
                Destroy(currentTile.gameObject);
                Invoke("UpdateTiles",0.1f);
            }
           
        }
        
        //Finish Tile
        if(currentTile.CompareTag("Finish"))
        {
            Canvas.GetComponent<Menus>().Won();
        }

        //Ice Tile
        if(currentTile.CompareTag("Ice"))
        {
            
            if (effectsAndItems.wet && !effectsAndItems.frost)
            {
                effectsAndItems.frost = true;
                effectsAndItems.wet = false;
                effectsAndItems.frostCount = 5;
            }
            mov.MoveDir(currentTile.transform.position - prevTile.transform.position);
            mov.sliding = true;
        }
        else mov.sliding = false;

        //Water Tile
        if (currentTile.CompareTag("Water"))
        {
            
            effectsAndItems.wet = true;
            effectsAndItems.gunpowder = false;
            if (effectsAndItems.electricity)
            {
                audioManager.Play("Electricity");
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
                audioManager.Play("Gunpowder");
                Canvas.GetComponent<Menus>().DisableDeathText();
                Canvas.GetComponent<Menus>().ExplosionDeath.SetActive(true);
            }
        }
        //Timebomb Tile
        if(prevTile.CompareTag("Timebomb") && effectsAndItems.gunpowder)
        {
            //make sure the player can't walk back on the cloud
            if(prevTileDir == "left")   mov.goleft = false;
            if(prevTileDir == "right")  mov.goright = false;
            if(prevTileDir == "up")     mov.goup = false;
            if(prevTileDir == "down")     mov.godown = false;
            //destroy cloud
            Destroy(prevTile.gameObject);
            effectsAndItems.gunpowder = false;
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
            effectsAndItems.poisonCount = 5; // amount of tiles you can travel before dying
        }
        //Antidote Tile
        if(currentTile.CompareTag("Antidote"))
        {
            effectsAndItems.poison = false;
        }
        

        //Cloud Tile
        if(prevTile.CompareTag("Cloud"))
        {
            //make sure the player can't walk back on the cloud
            if(prevTileDir == "left")   mov.goleft = false;
            if(prevTileDir == "right")  mov.goright = false;
            if(prevTileDir == "up")     mov.goup = false;
            if(prevTileDir == "down")   mov.godown = false;
            //destroy cloud
            Destroy(prevTile.gameObject);
        }
        //Cannon Tile
        if(currentTile.CompareTag("Cannon") && effectsAndItems.gunpowder)
        {
            mov.MoveDir(currentTile.gameObject.transform.GetChild(0).position - currentTile.transform.position);
            effectsAndItems.gunpowder = false;
            mov.Invoke("TilesUpdatedToFalse",0.4f);
        }

        //Flower tile
        if(currentTile.CompareTag("Flower") && effectsAndItems.wet)
        {
            //spawn new grass tiles on adjecent tiles
            Instantiate(grassTile, new Vector3(currentTile.transform.position.x - 1, 0f, currentTile.transform.position.z), currentTile.transform.rotation);
            Instantiate(grassTile, new Vector3(currentTile.transform.position.x + 1f, 0f, currentTile.transform.position.z), currentTile.transform.rotation);
            Instantiate(grassTile, new Vector3(currentTile.transform.position.x, 0f, currentTile.transform.position.z + 1f), currentTile.transform.rotation);
            Instantiate(grassTile, new Vector3(currentTile.transform.position.x, 0f, currentTile.transform.position.z - 1f), currentTile.transform.rotation);
            //destroy other tiles if there are any
            if(mov.goleft)  Destroy(leftTile.gameObject);
            if(mov.goright) Destroy(rightTile.gameObject);
            if(mov.goup)    Destroy(upTile.gameObject);
            if(mov.godown)  Destroy(downTile.gameObject);
            //wet effect gone and update tiles so the adjecent tiles are updated to be the grass tiles that were just created
            effectsAndItems.wet = false;
            UpdateTiles();
        }

        //Conveyor tile
        if(currentTile.CompareTag("Moving"))
        {
            mov.MoveDir(currentTile.gameObject.transform.GetChild(0).position - currentTile.transform.position);
            mov.moving = true;
        }
        else mov.moving = false;

        //Button tile

        if(currentTile.CompareTag("Button") && effectsAndItems.electricity)
        {
            GameObject[] triggers = GameObject.FindGameObjectsWithTag("Trigger");

            foreach (GameObject trigger in triggers)
            {
                trigger.GetComponent<ButtonTrig>().TurnOn();
            }
            effectsAndItems.electricity = false;
            UpdateTiles();
        }
    }

    public void UpdateWhenInput() // This fucntion gets called the frame the player tries to move
    {

        moves += 1;
        //Frost counter countdown
        if (effectsAndItems.frost)
        {
            effectsAndItems.frostCount -= 1;
        }
        if (effectsAndItems.frostCount <= 0)
        {
            audioManager.Play("Frost");
            Canvas.GetComponent<Menus>().DisableDeathText();
            Canvas.GetComponent<Menus>().FrostDeath.SetActive(true);
        }

        //Poison Tile
        if (effectsAndItems.poison)
        {
            effectsAndItems.poisonCount -= 1;
        }

        
        if(effectsAndItems.poisonCount <= 0 && effectsAndItems.frostCount > 0)
        {
            audioManager.Play("Poison");
            Canvas.GetComponent<Menus>().DisableDeathText();
            Canvas.GetComponent<Menus>().PoisonDeath.SetActive(true);
        }
    }
    void Respawn()
    {
        Canvas.GetComponent<Menus>().DisableDeathText();
        Canvas.GetComponent<Menus>().FellDeath.SetActive(true);
    }
}
