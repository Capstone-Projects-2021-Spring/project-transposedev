using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RocketBehaviour : MonoBehaviour
{
    public GunInfo itemInfo;
    float radius = 5;
    float power = 150;
    float explosiveLift = 3;
    public GameObject FireworksAll;

    // Start is called before the first frame update
    long initTime;
    void Start()
    {
        initTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    private void OnCollisionEnter(Collision collision)
    {
        long cr = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (initTime + 40 > cr)
        {
            return;
        }
        Explode();
        Vector3 rocketOrigin = transform.position;
        Collider[] colliders = Physics.OverlapSphere(rocketOrigin, radius);
        foreach (Collider hit in colliders)
        {
            hit.GetComponent<Collider>().gameObject.GetComponent<IDamageable>()?.TakeDamage(itemInfo.damage, this);
            if (hit.attachedRigidbody)
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(power, rocketOrigin, radius, explosiveLift);
            }
        }
        Destroy(gameObject);
    }

    void Explode()
    {
        GameObject firework = Instantiate(FireworksAll, transform.position, Quaternion.identity);
        firework.GetComponent<ParticleSystem>().Play();
    }
}
