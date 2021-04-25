using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : PlayerClass
{
    [SerializeField] private AudioClip teleportClip;
    [SerializeField] private AudioClip teleportFail;
    [SerializeField] private Camera cam;
    private AudioSource teleportSource;
    private AudioSource failSource;

    public int cooldown;

    public void Start()
    {
        teleportSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        teleportSource.playOnAwake = false;
        teleportSource.clip = teleportClip;

        failSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        failSource.playOnAwake = false;
        failSource.clip = teleportFail;

        cooldown = 0;
    }

    private void Update()
    {
        if (cooldown > 0)
            cooldown--;
    }

    public override void StopAbility()
	{
        // does nothing for teleporter
	}

	public override void UseAbility()
    {
        if(cam == null)
        {
            Debug.Log("No camera found");
            return;
        }

        if (cooldown <= 0)
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            ray.origin = cam.transform.position;
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                teleportSource.Play();
                Debug.Log("Raycast hit at " + hit.point);
                gameObject.transform.position = hit.point + new Vector3(0, 5, 0);
                cooldown = 500;
            }
        }
        else
        {
            failSource.Play();
        }

    }

    public override void UseAltAbility()
    {
        throw new System.NotImplementedException();
    }
}
