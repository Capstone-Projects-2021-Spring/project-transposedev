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

	void Shoot()
	{
		Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
		ray.origin = camera.transform.position;
		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
            
		}
	}
	public override void Release()
	{

	}
    public override void HoldDown()
    {
        
    }
}
