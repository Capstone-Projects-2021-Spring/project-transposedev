using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RocketBehaviour : MonoBehaviour
{
    public GunInfo itemInfo;
    float radius = 10;
    float power = 170;
    float explosiveLift = 4;
    // Start is called before the first frame update
    long initTime;
    void Start()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        DateTime dt = DateTime.Now;
        long cr = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (initTime + 1000 > cr)
        {
            return;
        }
        Vector3 rocketOrigin = transform.position;
        Collider[] colliders = Physics.OverlapSphere(rocketOrigin, radius);
        foreach (Collider hit in colliders)
        {
            hit.GetComponent<Collider>().gameObject.GetComponent<IDamageable>()?.TakeDamage(itemInfo.damage);
            if (hit.attachedRigidbody)
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(power, rocketOrigin, radius, explosiveLift);
            }
        }
        Destroy(gameObject);
    }

}
