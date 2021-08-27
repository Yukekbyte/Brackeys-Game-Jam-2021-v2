using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menus : MonoBehaviour
{
    public GameObject TilesMenu;
    GameObject Player;
    Camera cam;

    // Inside encyclopedia
    public GameObject tileButtons;
    public Button backButton;
    public TextMeshProUGUI title;
    public GameObject grassText;
    public GameObject waterText;
    public GameObject fireText;
    public GameObject iceText;
    public GameObject gunpowderText;
    public GameObject weaponsText;
    public GameObject enemyText;
    public GameObject poisonText;
    public GameObject antidoteText;
    public GameObject cloudText;
    public GameObject cannonText;
    public GameObject finishText;
    public GameObject flowerText;
    public GameObject timebombText;
    public GameObject zapText;
    public GameObject conveyerText;
    public GameObject buttonText;
    public GameObject activatableText;
    public GameObject activatedText;

    //inside deathscreen
    public GameObject deathscreen;
    public GameObject ExplosionDeath;
    public GameObject PoisonDeath;
    public GameObject FrostDeath;
    public GameObject EnemieDeath;
    public GameObject FellDeath;
    public GameObject ElecDeath;

    //inside winscreen
    public GameObject Winscreen;
    public TextMeshProUGUI Score;

    
    void Awake()
    {
        cam = FindObjectOfType<Camera>();
        TilesMenu.SetActive(false);
        deathscreen.SetActive(false);
        Winscreen.SetActive(false);
        Player = GameObject.Find("Wizard char");
    }
    public void Won()
    {
        Winscreen.SetActive(true);
        Score.text = "Moves: " + Player.GetComponent<TileDetect>().moves;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Retry()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void DisableDeathText()
    {
        Player.GetComponent<PlayerMovement>().godown = false;
        Player.GetComponent<PlayerMovement>().goup = false;
        Player.GetComponent<PlayerMovement>().goleft = false;
        Player.GetComponent<PlayerMovement>().goright = false;
        deathscreen.SetActive(true);
        ExplosionDeath.SetActive(false);
        PoisonDeath.SetActive(false);
        FrostDeath.SetActive(false);
        EnemieDeath.SetActive(false);
        FellDeath.SetActive(false);
        ElecDeath.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Encyclopedia()
    {
        if (TilesMenu.activeSelf)
        {
            TilesMenu.SetActive(false);
        }
        else
        {
            TilesMenu.SetActive(true);

            backButton.enabled = false;

            //all tile info text false
            grassText.SetActive(false);
            waterText.SetActive(false);
            fireText.SetActive(false);
            iceText.SetActive(false);
            gunpowderText.SetActive(false);
            weaponsText.SetActive(false);
            enemyText.SetActive(false);
            poisonText.SetActive(false);
            antidoteText.SetActive(false);
            cloudText.SetActive(false);
            cannonText.SetActive(false);
            finishText.SetActive(false);
            flowerText.SetActive(false);
            timebombText.SetActive(false);
            zapText.SetActive(false);
            conveyerText.SetActive(false);
            buttonText.SetActive(false);
            activatableText.SetActive(false);
            activatedText.SetActive(false);

        }
    }
    public void Preview()
    {
        cam.previewing = !cam.previewing;    
    }
    void TileButton()   //things that always have to happen when a tile button is pressed (not specific to the tile button pressed in that instance)
    {
        //disable the title and tile buttons and enable the back button
        title.enabled = false;
        tileButtons.SetActive(false);
        backButton.enabled = true;
    }
    public void BackButton()
    {
        if (tileButtons.activeSelf)
        {
            TilesMenu.SetActive(false);
        }
        else
        {
            //enable the title and tile buttons and disable the backbutton, reverse of TileButton()
            title.enabled = true;
            tileButtons.SetActive(true);
            

            //also disable all texts (slight spaghetti)
            grassText.SetActive(false);
            waterText.SetActive(false);
            fireText.SetActive(false);
            iceText.SetActive(false);
            gunpowderText.SetActive(false);
            weaponsText.SetActive(false);
            enemyText.SetActive(false);
            poisonText.SetActive(false);
            antidoteText.SetActive(false);
            cloudText.SetActive(false);
            cannonText.SetActive(false);
            finishText.SetActive(false);
            flowerText.SetActive(false);
            timebombText.SetActive(false);
            zapText.SetActive(false);
            conveyerText.SetActive(false);
            buttonText.SetActive(false);
            activatableText.SetActive(false);
            activatedText.SetActive(false);
        }
        
    }
    public void GrassButton()
    {
        grassText.SetActive(true);
        TileButton();
    }
    public void WaterButton()
    {
        waterText.SetActive(true);
        TileButton();
    }
    public void FireButton()
    {
        fireText.SetActive(true);
        TileButton();
    }
    public void IceButton()
    {
        iceText.SetActive(true);
        TileButton();
    }
    public void GunpowderButton()
    {
        gunpowderText.SetActive(true);
        TileButton();
    }
    public void WeaponsButton()
    {
        weaponsText.SetActive(true);
        TileButton();
    }
    public void EnemyButton()
    {
        enemyText.SetActive(true);
        TileButton();
    }
    public void PoisonButton()
    {
        poisonText.SetActive(true);
        TileButton();
    }
    public void AntidoteButton()
    {
        antidoteText.SetActive(true);
        TileButton();
    }
    public void CloudButton()
    {
        cloudText.SetActive(true);
        TileButton();
    }
    public void CannonButton()
    {
        cannonText.SetActive(true);
        TileButton();
    }
    public void FinishButton()
    {
        finishText.SetActive(true);
        TileButton();
    }
    public void FlowerButton()
    {
        flowerText.SetActive(true);
        TileButton();
    }
    public void TimebombButton()
    {
        timebombText.SetActive(true);
        TileButton();
    }
    public void ZapButton()
    {
        zapText.SetActive(true);
        TileButton();
    }
    public void ConveyerButton()
    {
        conveyerText.SetActive(true);
        TileButton();
    }
    public void ButtonButton()
    {
        buttonText.SetActive(true);
        TileButton();
    }
    public void ActivatableButton()
    {
        activatableText.SetActive(true);
        TileButton();
    }
    public void ActivatedButton()
    {
        activatedText.SetActive(true);
        TileButton();
    }
}
