using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketNoDamageBehavior : MonoBehaviour
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
        if (initTime + 20 > cr)
        {
            return;
        }
        Vector3 rocketOrigin = transform.position;
        Collider[] colliders = Physics.OverlapSphere(rocketOrigin, radius);
        Explode();
        foreach (Collider hit in colliders)
        {
            Debug.Log("Exploding on: " + hit.gameObject.name);
            //hit.GetComponent<Collider>().gameObject.GetComponent<IDamageable>()?.TakeDamage(itemInfo.damage);
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
