using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menus : MonoBehaviour
{
    public GameObject TilesMenu;
    

    // Inside encyclopedia
    public GameObject tileButtons;
    public Button backButton;
    public TextMeshProUGUI title;
    public GameObject grassText;
    public GameObject waterText;
    
    void Awake()
    {
        TilesMenu.SetActive(false);
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
        }
    }
    public void Preview()
    {

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
        //enable the title and tile buttons and disable the backbutton, reverse of TileButton()
        title.enabled = true;
        tileButtons.SetActive(true);
        backButton.enabled = false;

        //also disable all texts (slight spaghetti)
        grassText.SetActive(false);
        waterText.SetActive(false);
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

}
