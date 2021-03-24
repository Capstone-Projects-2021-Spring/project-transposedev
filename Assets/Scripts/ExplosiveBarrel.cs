using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour, IDamageable {
    public GameObject Barrel;
    public GameObject explosionEffect;

    [SerializeField]
    private float explosionRange;
    private float targetDamage = 100;
    private float barrelHealth = 50;

    private void Awake() {
        Barrel.SetActive(true);
    }

    public void TakeDamage(float damage) {
        barrelHealth -= damage;
        Debug.Log("Ouch. Barrel is hurt. Health:" +  barrelHealth);
    }

    public void Explode() {

        Debug.Log("BOOOM!");

        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] targets = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (Collider target in targets) {
            if (target.GetComponent<Target>() != null) { // Check it's a target
                target.GetComponent<Target>().TakeDamage(targetDamage); // Damage the targets
            }
        }

        Destroy(Barrel);
    }

    private void Update() {
        if (barrelHealth <= 0) {
            Explode();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
