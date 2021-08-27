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
    public bool frost;
    private bool _frost;
    public bool gunpowder;
    private bool _gunpowder;
    public bool electricity;
    private bool _electricity;
    public string EffectsList;
    public string FrostIcon = " <sprite=0>           ";
    public string GunpowderIcon = " <sprite=1> ";
    public string PoisonIcon = "<sprite=3>";
    public string ElectricityIcon = "<sprite=2>";
    public string WetIcon = "<sprite=4>";
    public string WeaponIcon = "<sprite=5>";
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
        _electricity = false;
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
                EffectsList += FrostIcon;
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
                EffectsList += WetIcon;
            }
            if (!wet)
            {
                EffectsList = EffectsList.Replace(WetIcon, "");
            }
            _wet = wet;
        }

        if (poison != _poison)
        {
            
            if (poison)
            {
                
                EffectsList += PoisonIcon;
            }
            if (!poison)
            {
                
                EffectsList = EffectsList.Replace(PoisonIcon, "");
            }
            _poison = poison;
        }

        if (electricity != _electricity)
        {

            if (electricity)
            {

                EffectsList += ElectricityIcon;
            }
            if (!electricity)
            {

                EffectsList = EffectsList.Replace(ElectricityIcon, "");
            }
            _electricity = electricity;
        }

        if (gunpowder != _gunpowder)
        {

            if (gunpowder)
            {

                EffectsList += GunpowderIcon;
            }
            if (!gunpowder)
            {

                EffectsList = EffectsList.Replace(GunpowderIcon, "");
            }
            _gunpowder = gunpowder;
        }
    }
    
}
