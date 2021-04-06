using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour, IDamageable {
    //public GameObject Barrel;
    public GameObject explosionEffect;
    public AudioSource explosionSound;

    [SerializeField]
    private float explosionRange;
    private float targetDamage = 100;
    private float barrelHealth = 50;
    private bool played = false;

    private void Awake() {
    }

    public void TakeDamage(float damage) {
        barrelHealth -= damage;
        //Debug.Log("Ouch. Barrel is hurt. Health:" +  barrelHealth);
    }

    public void Explode() {

        if (!played) {
            Instantiate(explosionEffect, transform.position, transform.rotation);
            explosionSound.Play();
            played = true;
        }

        Collider[] targets = Physics.OverlapSphere(transform.position, explosionRange);
        foreach (Collider target in targets) {
            if (target.gameObject.GetComponent<PlayerMovement>() != null) { // Check it's a player
                //target.GetComponent<Target>().TakeDamage(targetDamage); // Damage the player
                //Debug.Log("Damaged Player!");
            }
        }
        GameManager.Instance.DestroyHazard(gameObject);
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
