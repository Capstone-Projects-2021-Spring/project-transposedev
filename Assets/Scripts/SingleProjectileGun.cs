using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	
		
	}
}
