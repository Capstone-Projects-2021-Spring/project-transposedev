using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class RailGun : Gun
{
    // reference to player camera
    [SerializeField] Camera camera;

    [SerializeField] private AudioClip myClip;
    [SerializeField] private AudioClip failClip;

    private AudioSource mySource;
    private AudioSource failSource;
    [SerializeField] private Transform railPoint;

    private int cooldown;

    LineRenderer railRenderer;


    private void Awake()
    {
        mySource = gameObject.AddComponent<AudioSource>() as AudioSource;
        mySource.playOnAwake = false;
        mySource.clip = myClip;

        failSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        failSource.playOnAwake = false;
        failSource.clip = failClip;

        railRenderer = gameObject.GetComponent<LineRenderer>() as LineRenderer;

        railRenderer.startColor = Color.cyan;
        railRenderer.endColor = Color.blue;
    }

    private void Update()
    {
        railRenderer.endWidth *= .75f;
        railRenderer.startWidth *= .75f;

        if(cooldown > 0)
            cooldown--;
    }

    public override void Use()
    {
        if (cooldown <= 0)
        {
            mySource.Play();
            Shoot();
            cooldown = 60;
        }
        else
        {
            failSource.Play();
        }
    }

    void Shoot()
    {
        railRenderer.startWidth = 1;
        railRenderer.endWidth = 1;

        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = camera.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            railRenderer.SetPosition(0, railPoint.position);
            railRenderer.SetPosition(1, hit.point);

            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage, this);
        }
        else
        {
            railRenderer.SetPosition(0, railPoint.position);
            railRenderer.SetPosition(1, railPoint.forward * 400 + railPoint.position);;
        }
    }
    public override void Release()
    {

    }
    public override void HoldDown()
    {

    }

}

