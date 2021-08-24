using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsAndItems : MonoBehaviour
{
    // all effects
    public bool wet;
    public bool weapon;
    public bool poison;
    public int poisonCount = 1;
    public int frostCount = 1;
    public int wetCount = 1;
    public bool frost;
    public bool gunpowder;
    public bool electricity;
    public string[] EffectList;
    // Start is called before the first frame update
    void Start()
    {
        wet = false;
        weapon = false;
        poison = false;
        frost = false;
        gunpowder = false;
        electricity = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
