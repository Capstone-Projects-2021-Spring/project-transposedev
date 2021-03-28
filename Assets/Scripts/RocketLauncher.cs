using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RocketLauncher : Gun
{
	// reference to player camera
	[SerializeField] Camera camera;

    [SerializeField] private AudioClip myClip;
    private AudioSource mySource;
    public GameObject rocket;
    public float speed = 5;

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
        GameObject instantiatedProjectile = (GameObject)Instantiate(rocket, transform.position, transform.rotation);
        instantiatedProjectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        Destroy(instantiatedProjectile,3);
    }

    public override void Release()
	{

	}
    public override void HoldDown()
    {
        
    }
}
