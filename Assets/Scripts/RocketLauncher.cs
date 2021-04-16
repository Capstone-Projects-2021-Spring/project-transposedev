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
    //for automatic fire
    private long time_fire = 0;
    private long cooldown = 500;
    private void Awake()
    {
        mySource = gameObject.AddComponent<AudioSource>() as AudioSource;
        mySource.playOnAwake = false;
        mySource.clip = myClip;
        mySource.volume = mySource.volume / 2;
    }

    public override void Use()
	{
        mySource.Play();
		Shoot();
	}

	void Shoot()
	{
        DateTime dt = DateTime.Now;
        long cr = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (isReloading)
        {
            if (cr < time_reload)
            {
                return;
            }
            ReloadCompleted();
        }
        if (!Reload())
        {
            return;
        }
        GameObject instantiatedProjectile = (GameObject)Instantiate(rocket, transform.position, transform.rotation);
        instantiatedProjectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        Destroy(instantiatedProjectile,3);
        ammo_current--;
        if (ammo_current <= 0)
        {
            isReloading = true;
            time_reload = cr + reload;
        }
    }
    bool Reload()
    {
        DateTime dt = DateTime.Now;
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
    public override void Release()
	{

	}
    public override void HoldDown()
    {
        
    }
}
