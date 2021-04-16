using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class HUD : MonoBehaviour
{
    public static int health = 100;
    public static int ammo = 30;
    public static int armor = 100;
    //public static int ammo_BackUp = 80;
    public static int trgt_cntr;
    public static string timer = "";

    public Transform CurrentAmmoText;
    //public Transform BackUpAmmoText;
    public Transform HealthText;
    public Transform ArmorText;
    public Transform TimerText;
    public Transform TargetCounterText;

    public int getHealth()
    {
        return transform.gameObject.GetComponent<PlayerStats>().GetHealth();
    }
    public static String getTime()
    {
        if (GameObject.Find("RuleManager") != null)
            return GameObject.Find("RuleManager").GetComponent<RuleSet>().GetTime();
        else
            return "";
    }

    public static int getTargets()
    {
        if (GameObject.Find("RuleManager") != null)
            return GameObject.Find("RuleManager").GetComponent<RuleSet>().TargetCounter();
        else
            return 0;
    }

    public int getRemainAmmo()
    {
        int index = transform.gameObject.GetComponent<PlayerMovement>().getItemIndex();
        switch (index)
        {
            case 0:
                return transform.gameObject.GetComponent<PlayerMovement>().getCurrentItem().GetComponent<SingleShotGun>().getRemainingAmmo();
            case 1:
                return transform.gameObject.GetComponent<PlayerMovement>().getCurrentItem().GetComponent<SingleShotGun>().getRemainingAmmo();
            case 2:
                return transform.gameObject.GetComponent<PlayerMovement>().getCurrentItem().GetComponent<AutomaticGun>().getRemainingAmmo();
            case 3:
                return transform.gameObject.GetComponent<PlayerMovement>().getCurrentItem().GetComponent<RocketLauncher>().getRemainingAmmo();
            default:
                return transform.gameObject.GetComponent<PlayerMovement>().getCurrentItem().GetComponent<SingleShotGun>().getRemainingAmmo();
        }
    }
    /*
    public static int getBackUpAmmo()
    {
        return ammo_BackUp;

    }
    */
    public int getArmor()
    {
        return transform.gameObject.GetComponent<PlayerStats>().GetArmor();
        //return armor;
    }
    void Start()
    {

    }

    void Update()
    {
        
        health = getHealth();
        armor = getArmor();
        ammo = getRemainAmmo();
        //ammo_BackUp = getBackUpAmmo();
        timer = getTime();
        trgt_cntr = getTargets();

        CurrentAmmoText.GetComponent<Text>().text = "" + ammo;
        //BackUpAmmoText.GetComponent<Text>().text = "" + ammo_BackUp;
        HealthText.GetComponent<Text>().text = "" + health;
        ArmorText.GetComponent<Text>().text = "" + armor;
        TimerText.GetComponent<Text>().text =  timer;
        TargetCounterText.GetComponent<Text>().text = "Targets Destroyed: " + trgt_cntr;
        
    }
}
