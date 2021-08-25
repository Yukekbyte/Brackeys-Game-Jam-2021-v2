using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menus : MonoBehaviour
{
    public GameObject TilesMenu;
    
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
        }
    }
    public void Preview()
    {

    }
}
