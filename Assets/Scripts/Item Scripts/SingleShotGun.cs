using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using System;
public class SingleShotGun : Gun
{
	// reference to player camera
	[SerializeField] Camera camera;

    [SerializeField] private AudioClip myClip;
    private AudioSource mySource;
    //reloadtime
    private long time_reload = 0;
    private long reload = 10000;
    private bool isReloading;
    //ammo
    private int ammo_max = 20;
    private int ammo_current = 20;
    private long time_fire = 0;
    private long cooldown = 100;
    private void Awake()
    {
        mySource = gameObject.AddComponent<AudioSource>() as AudioSource;
        mySource.playOnAwake = false;
        mySource.clip = myClip;
        mySource.volume = mySource.volume / 2;
    }

    public override bool Use()
	{
		return Shoot();
	}
    void Update()
    {
        //check if reloading
        long cr = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (isReloading)
        {
            if (cr < time_reload)
            {
                return;
            }
            ReloadCompleted();
        }
        //check if reloading end
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
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
		ray.origin = camera.transform.position;
		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage, this);
            
		}
        ammo_current--;
        if (ammo_current <= 0)
        {
            isReloading = true;
            time_reload = cr + reload;
        }
        return true;
	}
	public override bool Release()
	{
		return false;
	}
    public override bool HoldDown()
    {
		return false;
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
    public int getRemainingAmmo()
    {
        return ammo_current;
    }
}
