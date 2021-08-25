using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UImanager : MonoBehaviour
{
    Canvas canvas;
    // Start is called before the first frame update
    void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
