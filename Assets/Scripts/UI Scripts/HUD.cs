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
    public static int ammo_BackUp = 80;
    public static string timer = "";

    public Transform CurrentAmmoText;
    public Transform BackUpAmmoText;
    public Transform HealthText;
    public Transform ArmorText;
    public Transform TimerText;

    public GameObject ESCMenu = null;

    public static int getHealth()
    {
        return health;
    }
    public static String getTime()
    {
        int min = 0;
        int sec = 0;
        //testing, please edit this for hooking timer
        //return ""+min+":"+sec;
        return DateTime.Now.ToString("h:mm:ss tt"); 
    }

    //please hook the following to Game Status
    public static int getRemainAmmo()
    {
        return ammo;

    }
    public static int getBackUpAmmo()
    {
        return ammo_BackUp;

    }
    public static int getArmor()
    {
        return armor;
    }
    void Start()
    {
        health = getHealth();
        armor = getArmor();
        ammo = getRemainAmmo();
        ammo_BackUp = getBackUpAmmo();
    }


    //testing features(please remove this after hooking to the game status)
    private static long time = 0;
    private static long time_reload = 3000;
    private static int mag = 30;
    //testing features end
    void Update()
    {
        //testing features (please remove this after hooking to the game status)
        DateTime dt = DateTime.Now;
        long cr = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (!EscMenu.isInEscMenu()) { //test if Esc menu is up
            if (Input.GetButtonDown("Fire1"))
            {
                if (ammo > 0) {
                    ammo -= 1;
                }
                if (ammo <= 0)
                {
                    if (ammo_BackUp > 0) {
                        if (cr > time)
                        {
                            time = cr + time_reload;
                        }
                    }
                }
            }
        }
        if (ammo <= 0)
        {
            if (cr >= time)
            {
                if (mag < ammo_BackUp)
                {
                    ammo = mag;
                    ammo_BackUp -= mag;
                }
                else
                {
                    ammo = ammo_BackUp;
                    ammo_BackUp = 0;
                }
            }
   
        }
        //testing features end

        health = getHealth();
        armor = getArmor();
        ammo = getRemainAmmo();
        ammo_BackUp = getBackUpAmmo();
        timer = getTime();
        CurrentAmmoText.GetComponent<Text>().text = "" + ammo;
        BackUpAmmoText.GetComponent<Text>().text = "" + ammo_BackUp;
        HealthText.GetComponent<Text>().text = "" + health;
        ArmorText.GetComponent<Text>().text = "" + armor;
        TimerText.GetComponent<Text>().text =  timer;

        //to call Esc Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (EscMenu.isInEscMenu())
            {
                ESCMenu.SetActive(false);
                EscMenu.SetInEscMenu(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

            }
            else
            {
                ESCMenu.SetActive(true);
                EscMenu.SetInEscMenu(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}
