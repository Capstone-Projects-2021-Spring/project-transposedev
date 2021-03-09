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
    public static int getHealth()
    {
        return health;
    }
    public static String getTime()
    {
        string min = "0";
        string sec = "0";
        //return min+":"+sec;
        return DateTime.Now.ToString("h:mm:ss tt");
    }
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
    // Start is called before the first frame update
    void Start()
    {
        //read ammo,health,armor from other scrpit or class
        health = getHealth();
        armor = getArmor();
        ammo = getRemainAmmo();
        ammo_BackUp = getBackUpAmmo();
    }

    // Update is called once per frame

    //testing features
    private static long time = 0;
    private static long time_reload = 3000;
    private static int mag = 30;
    //testing features end
    void Update()
    {
        //testing features
        DateTime dt = DateTime.Now;
        long cr = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        Debug.Log(time + " " + cr);
        if (!EscMenu.isInEscMenu()) { 
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
        else
        {
            Debug.Log("You are in ESC Menu now!");
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
        //ESC Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("You have called the Esc Menu!");
            SceneManager.LoadScene("EscMenu", LoadSceneMode.Additive);
        }
    }
}
