using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GrenadeNoDamageBehavior : MonoBehaviour
{
    public GunInfo itemInfo;
    float radius = 5;
    float power = 150;
    float explosiveLift = 3;
    public GameObject FireworksAll;
    // Start is called before the first frame update
    long initTime;
    bool trigger = false;
    void Start()
    {
        initTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
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
        */

        StartCoroutine(ExplodeInSeconds(2));

        if (collision.gameObject.GetComponent<IDamageable>() != null)
            Explode();

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
            Debug.Log("Exploding on: " + hit.gameObject.name);
            //hit.GetComponent<Collider>().gameObject.GetComponent<IDamageable>()?.TakeDamage(itemInfo.damage, this);
            if (hit.attachedRigidbody)
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(power, rocketOrigin, radius, explosiveLift);
            }
        }
        Destroy(gameObject);
    }
}
