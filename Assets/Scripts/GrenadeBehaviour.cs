using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GrenadeBehaviour : MonoBehaviour
{
    public GunInfo itemInfo;
    float radius = 5;
    float power = 150;
    float explosiveLift = 3;
    public GameObject FireworksAll;
    bool trigger = false;
    // Start is called before the first frame update
    long initTime;
    void Start()
    {
        initTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (trigger)
        {
            return;
        }
        long cr = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (initTime + 50 > cr)
        {
            return;
        }
        trigger = true;
        StartCoroutine(ExplodeInSeconds(3));
        //Explode();
    }
    IEnumerator ExplodeInSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        Explode();
        yield break;
    }
    void Explode()
    {
        Vector3 rocketOrigin = transform.position;
        Collider[] colliders = Physics.OverlapSphere(rocketOrigin, radius);

        GameObject firework = Instantiate(FireworksAll, rocketOrigin, Quaternion.identity);
        firework.GetComponent<ParticleSystem>().Play();

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
}
