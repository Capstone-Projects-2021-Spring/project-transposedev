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
        rb.useGravity = false;
    }

    public override void UseAltAbility()
    {
        throw new System.NotImplementedException();
    }

	public override void StopAbility()
	{
        gravitySource.Play();
        rb.useGravity = true;
    }
}
