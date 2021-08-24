using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EffectsAndItems : MonoBehaviour
{
    // all effects
    public TextMeshProUGUI List;
    public bool wet;
    private bool _wet;
    public bool weapon;
    public bool poison;
    private bool _poison;
    public int poisonCount = 1;
    public int frostCount = 1;
    public int wetCount = 1;
    public bool frost;
    private bool _frost;
    public bool gunpowder;
    public bool electricity;
    public string EffectsList;
    public string FrostIcon = "<sprite=0>";
    public string GunpowderIcon = "<sprite=1>";
    public string PoisonIcon = "<sprite=3>";
    public string ElectricityIcon = "<sprite=3>";
    public string WetIcon = "<sprite=4>";
    // Start is called before the first frame update
    void Start()
    {
        // reseting all values at load of scene
        wet = false;
        weapon = false;
        poison = false;
        frost = false;
        gunpowder = false;
        electricity = false;
        _frost = false;
        _poison = false;
        _wet = false;
    }

    // Update is called once per frame
    void Update()
    {
        List.text = EffectsList;
        // changing the text
        if(frost != _frost)
        {
            if (frost)
            {
                EffectsList = EffectsList + FrostIcon;
            }
            if (!frost)
            {
                EffectsList = EffectsList.Replace(FrostIcon, "");
            }
            _frost = frost;
        }

        if (wet != _wet)
        {
            if (wet)
            {
                EffectsList = EffectsList + WetIcon;
            }
            if (!wet)
            {
                EffectsList = EffectsList.Replace(WetIcon, "");
            }
            _wet = wet;
        }

        if (poison != _poison)
        {
            print("1");
            if (poison)
            {
                print(PoisonIcon);
                EffectsList += FrostIcon;
            }
            if (!poison)
            {
                print("3");
                EffectsList = EffectsList.Replace(PoisonIcon, "");
            }
            _poison = poison;
        }
    }
    
}
