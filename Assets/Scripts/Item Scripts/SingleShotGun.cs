using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class SingleShotGun : Gun
{
	// reference to player camera
	[SerializeField] Camera camera;

    [SerializeField] private AudioClip myClip;
    private AudioSource mySource;
    private void Awake()
    {
        mySource = gameObject.AddComponent<AudioSource>() as AudioSource;
        mySource.playOnAwake = false;
        mySource.clip = myClip;
    }

    public override void Use()
	{
        mySource.Play();
		Shoot();
	}
<<<<<<< Updated upstream

	void Shoot()
=======
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
>>>>>>> Stashed changes
	{
		Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
		ray.origin = camera.transform.position;
		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage, this);
            
		}
	}
	public override void Release()
	{

	}
    public override void HoldDown()
    {
        
    }
    public int getRemainingAmmo()
    {
        return ammo_current;
    }
}
