using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class RocketLauncher : Gun
{
	// reference to player camera
	[SerializeField] Camera camera;

    [SerializeField] private AudioClip myClip;
    private AudioSource mySource;
    public GameObject rocket;
    public float speed = 5;
    //reloadtime
    private long time_reload = 0;
    private long reload = 10000;
    private bool isReloading;
    //ammo
    private int ammo_max = 1;
    private int ammo_current = 1;
    private long time_fire = 0;
    private long cooldown = 500;
    private void Awake()
    {
        mySource = gameObject.AddComponent<AudioSource>() as AudioSource;
        mySource.playOnAwake = false;
        mySource.clip = myClip;
    }

    public override bool Use()
	{
		return Shoot();
	}

    bool Shoot()
	{
        long cr = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (isReloading)
        {
            if (cr < time_reload)
            {
                return false;
            }
            ReloadCompleted();
        }
        if (cr < time_fire)
        {
            return false;
        }
        time_fire = cr + cooldown;
        if (!Reload())
        {
            return false;
        }
        mySource.Play();
        RaycastHit hit;
        GameObject instantiatedProjectile;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1)) {
            instantiatedProjectile = (GameObject)Instantiate(rocket, transform.position, transform.rotation);//this will make rocket to shoot from the start point in front of player but will cause it to pass through wall when player stand in front of wall
        }
        else
        {
            instantiatedProjectile = (GameObject)Instantiate(rocket, transform.position + transform.forward * 1, transform.rotation);
        }

        instantiatedProjectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        Destroy(instantiatedProjectile,3);
        ammo_current--;
        if (ammo_current <= 0)
        {
            isReloading = true;
            time_reload = cr + reload;
        }
        return true;
    }
    bool Reload()
    {
        long cr = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (ammo_current <= 0)
        {
            isReloading = true;
            time_reload = cr + reload;
            return false;
        }
        return true;
    }
    void ReloadCompleted()
    {
        //play reloaded sound
        ammo_current = ammo_max;
        isReloading = false;
    }
    public override bool Release()
	{
        return false;
	}
    public override bool HoldDown()
    {
        return false;
    }
}
