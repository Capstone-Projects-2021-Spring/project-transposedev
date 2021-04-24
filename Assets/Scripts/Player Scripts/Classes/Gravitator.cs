using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitator : PlayerClass
{
    private Rigidbody rb;
    [SerializeField] private AudioClip gravityClip;
    private AudioSource gravitySource;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        gravitySource = gameObject.AddComponent<AudioSource>() as AudioSource;
        gravitySource.playOnAwake = false;
        gravitySource.clip = gravityClip;
    }

    public override void UseAbility()
    {
        gravitySource.Play();

        if (Input.GetButtonDown("Fire3"))
        {
            gravitySource.Play();
            rb.useGravity = false;
        }
        else if (Input.GetButtonUp("Fire3"))
        {
            gravitySource.Play();
            rb.useGravity = true;
        }
    }

    public override void UseAltAbility()
    {
        throw new System.NotImplementedException();
    }
}
