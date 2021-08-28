using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
